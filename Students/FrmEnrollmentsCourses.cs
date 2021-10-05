using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Students.Model;
using Students.Model.Interfaces;
using Students.Properties;

namespace Students
{
    public partial class FrmEnrollmentsCourses : Form
    {
        private readonly ICourseService _courseService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly ILogger _logger;
        public event ChangeEventHandler Change;

        private void OnChange(string message)
        {
            Change?.Invoke(new ChangeEventArgs(message));
        }

        private void OnChange(ChangeEventArgs e)
        {
            OnChange(e.Message);
        }

        public FrmEnrollmentsCourses()
        {
            InitializeComponent();
            _courseService = Model.Services.Services.ServiceProvider.GetService<ICourseService>();
            _enrollmentService = Model.Services.Services.ServiceProvider.GetService<IEnrollmentService>();
            _logger = LogManager.GetCurrentClassLogger();
        }

        private void InitializeControls()
        {
            InitializeButtons();
            InitializeDgvStudents();
            InitializeDgvCourses();
        }

        private void InitializeButtons()
        {
            BtnAdd.Image = Resources.plus;
        }

        private const string StudentPersonalIdCol = "student_personal_id";
        private const string StudentDisplayCol = "student_display";

        private void InitializeDgvStudents()
        {
            DgvStudents.Columns.AddRange(
                new DataGridViewTextBoxColumn
                {
                    Name = StudentDisplayCol,
                    HeaderText = "Full name",
                    ValueType = typeof(string)
                },
                new DataGridViewTextBoxColumn
                {
                    Name = StudentPersonalIdCol,
                    HeaderText = "Personal ID",
                    ValueType = typeof(string)
                }
            );

            DGVConfigurator.ConfigureDataGridView(ref DgvStudents);
        }

        private const string CourseNumberCol = "course_number";
        private const string CourseNameCol = "course_name";

        private void InitializeDgvCourses()
        {
            DgvCourses.Columns.AddRange(
                new DataGridViewTextBoxColumn
                {
                    Name = CourseNumberCol,
                    HeaderText = "Number",
                    ValueType = typeof(string)
                },
                new DataGridViewTextBoxColumn
                {
                    Name = CourseNameCol,
                    HeaderText = "Name",
                    ValueType = typeof(string)
                }
            );
            DGVConfigurator.ConfigureDataGridView(ref DgvCourses);
            DgvCourses.RowEnter += DgvCourses_RowEnterAsync;
            foreach (DataGridViewColumn column in DgvCourses.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private async void DgvCourses_RowEnterAsync(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvCourses.SelectedRows.Count == 0)
                return;

            var row = DgvCourses.SelectedRows[0];
            var courseId = (long)row.Tag;
            await LoadStudentsAsync(courseId);
        }

        private async void FrmEnrollmentsCourses_LoadAsync(object sender, EventArgs e)
        {
            InitializeControls();
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            await LoadCoursesAsync();
            DgvStudents.Rows.Clear();
        }

        private async Task LoadCoursesAsync()
        {
            var courses = _courseService.GetAllAsync();
            DgvCourses.Rows.Clear();
            foreach (var course in await courses)
            {
                var row = DgvCourses.Rows[DgvCourses.Rows.Add()];
                row.Cells[CourseNumberCol].Value = course.Number;
                row.Cells[CourseNameCol].Value = course.Name;
                row.Tag = course.Id;
            }
        }

        private async Task LoadStudentsAsync(long courseId)
        {
            try
            {
                var studentsForCourseTask = _enrollmentService.GetStudentsForCourseAsync(courseId);
                DgvStudents.Rows.Clear();
                var students = await studentsForCourseTask;
                foreach (var student in students)
                {
                    var row = DgvStudents.Rows[DgvStudents.Rows.Add()];
                    row.Cells[StudentDisplayCol].Value = $"{student.LastName} {student.FirstName}";
                    row.Cells[StudentPersonalIdCol].Value = student.PersonalId;
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Load students");
                OnChange("Couldn't load students for course");
            }
        }

        private async void BtnAdd_ClickAsync(object sender, EventArgs e)
        {
            var f = new FrmEnrollmentEdit
            {
                StartPosition = FormStartPosition.CenterParent,
                WindowState = FormWindowState.Normal
            };
            f.Change += OnChange;
            if (f.ShowDialog() == DialogResult.OK)
            {
                await LoadDataAsync();
            }
        }
    }
}