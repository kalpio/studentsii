using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Students.Model;
using Students.Model.Entities;
using Students.Model.Interfaces;

namespace Students
{
    public partial class FrmEnrollmentEdit : Form
    {
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        private readonly IEnrollmentService _enrollmentService;
        private Student _student;
        public event ChangeEventHandler Change;

        private void OnChange(string message)
        {
            Change?.Invoke(new ChangeEventArgs(message));
        }

        public FrmEnrollmentEdit()
        {
            _studentService = Model.Services.Services.ServiceProvider.GetService<IStudentService>();
            _courseService = Model.Services.Services.ServiceProvider.GetService<ICourseService>();
            _enrollmentService = Model.Services.Services.ServiceProvider.GetService<IEnrollmentService>();
            InitializeComponent();
        }

        private async void FrmEnrollmentEdit_Load(object sender, EventArgs e)
        {
            var loadStudentTask = LoadStudents();
            InitializeControls();
            var loadCoursesTask = LoadCourses();

            await Task.WhenAll(loadStudentTask, loadCoursesTask);
        }

        private void InitializeControls()
        {
            DgvCourses.Columns.AddRange(
                new DataGridViewCheckBoxColumn
                {
                    Name = CheckBoxCol,
                    HeaderText = "",
                    ValueType = typeof(bool)
                },
                new DataGridViewTextBoxColumn
                {
                    Name = IdCol,
                    HeaderText = "#",
                    ValueType = typeof(long)
                },
                new DataGridViewTextBoxColumn
                {
                    Name = NumberCol,
                    HeaderText = "Number",
                    ValueType = typeof(string)
                },
                new DataGridViewTextBoxColumn
                {
                    Name = NameCol,
                    HeaderText = "Course name",
                    ValueType = typeof(string)
                }
            );
            DGVConfigurator.ConfigureDataGridView(ref DgvCourses);
            DgvCourses.KeyDown += DgvCourses_KeyDown;
            DgvCourses.CellContentClick += DgvCourses_CellContentClick;
        }

        private void DgvCourses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var colIndex = DgvCourses.Columns[CheckBoxCol]?.Index;
            if (e.ColumnIndex != colIndex)
                return;

            CheckCourse(e.RowIndex);
        }

        private void DgvCourses_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    CheckCourse();
                    break;
            }
        }

        private void CheckCourse(int rowIndex = 0)
        {
            switch (rowIndex)
            {
                case 0 when DgvCourses.SelectedRows.Count == 0:
                    return;
                case 0:
                    rowIndex = DgvCourses.SelectedRows[0].Index;
                    break;
            }

            var row = DgvCourses.Rows[rowIndex];
            row.Cells[CheckBoxCol].Value = !(bool)row.Cells[CheckBoxCol].Value;
        }

        private async Task LoadStudents()
        {
            var students = _studentService.GetAllAsync();
            CmbStudent.Items.Clear();
            foreach (var student in await students)
            {
                var item = new ComboBoxStudentItem
                {
                    Student = student
                };
                CmbStudent.Items.Add(item);
            }
        }

        private const string CheckBoxCol = "chk_course";
        private const string IdCol = "id";
        private const string NameCol = "name";
        private const string NumberCol = "number";

        private async Task LoadCourses()
        {
            var courses = _courseService.GetAllAsync();
            DgvCourses.Rows.Clear();
            foreach (var course in await courses)
            {
                var row = DgvCourses.Rows[DgvCourses.Rows.Add()];
                row.Cells[CheckBoxCol].Value = false;
                row.Cells[IdCol].Value = course.Id;
                row.Cells[NameCol].Value = course.Name;
                row.Cells[NumberCol].Value = course.Number;
                row.Tag = course.Id;
            }
        }

        private void CmbStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbStudent.SelectedItem is not ComboBoxStudentItem studentItem)
                return;

            _student = studentItem.Student;
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (_student == null)
                return;

            var courses = GetSelectedCourseIds();
            var tasks = new List<Task<bool>>();
            foreach (var course in courses)
            {
                var enrollment = new Enrollment
                {
                    CourseId = course,
                    StudentId = _student.Id,
                    EnrollmentDate = DateTime.Now
                };
                tasks.Add(_enrollmentService.SaveAsync(enrollment));
            }

            var result = true;
            while (tasks.Count > 0)
            {
                var finishedTask = await Task.WhenAny(tasks);
                if (!finishedTask.Result)
                    result = false;

                tasks.Remove(finishedTask);
            }

            if (!result)
                OnChange("Couldn't save all courses for student");

            DialogResult = DialogResult.OK;
            Close();
        }

        private IEnumerable<long> GetSelectedCourseIds()
        {
            return (from DataGridViewRow row in DgvCourses.Rows
                where (bool)row.Cells[CheckBoxCol].Value
                select (long)row.Tag).ToList();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }

    class ComboBoxStudentItem
    {
        public Student Student { get; set; }

        public override string ToString()
        {
            return $"{Student.LastName} {Student.FirstName} [{Student.PersonalId}]";
        }
    }
}