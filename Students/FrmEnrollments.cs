using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Students.Model;
using Students.Model.Interfaces;
using Students.Properties;

namespace Students
{
    public partial class FrmEnrollments : Form
    {
        private readonly IStudentService _studentService;
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

        public FrmEnrollments()
        {
            InitializeComponent();
            _studentService = Model.Services.Services.ServiceProvider.GetService<IStudentService>();
            _courseService = Model.Services.Services.ServiceProvider.GetService<ICourseService>();
            _enrollmentService = Model.Services.Services.ServiceProvider.GetService<IEnrollmentService>();
            _logger = LogManager.GetCurrentClassLogger();
        }

        private async void FrmEnrollments_LoadAsync(object sender, EventArgs e)
        {
            InitializeControls();
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            await LoadStudentsAsync();
            DgvCourses.Rows.Clear();
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

        private const string StudentPersonalId = "student_personal_id";
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
                    Name = StudentPersonalId,
                    HeaderText = "Personal ID",
                    ValueType = typeof(string)
                }
            );

            DGVConfigurator.ConfigureDataGridView(ref DgvStudents);
            foreach (DataGridViewColumn column in DgvStudents.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            DgvStudents.RowEnter += DgvStudents_RowEnter;
        }

        private void DgvStudents_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvStudents.SelectedRows.Count == 0)
                return;

            var row = DgvStudents.SelectedRows[0];
            var studentId = (long)row.Tag;
            LoadCourses(studentId).ConfigureAwait(false);
        }

        private const string CourseCheckBoxCol = "course_checkbox";
        private const string CourseNumberCol = "course_number";
        private const string CourseNameCol = "course_name";

        private void InitializeDgvCourses()
        {
            DgvCourses.Columns.AddRange(
                new DataGridViewCheckBoxColumn
                {
                    Name = CourseCheckBoxCol,
                    HeaderText = "",
                    ValueType = typeof(bool)
                },
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
        }

        private async Task LoadStudentsAsync()
        {
            var students = _studentService.GetAllAsync();
            DgvStudents.Rows.Clear();
            foreach (var student in await students)
            {
                var row = DgvStudents.Rows[DgvStudents.Rows.Add()];
                row.Cells[StudentPersonalId].Value = student.PersonalId;
                row.Cells[StudentDisplayCol].Value = $"{student.LastName} {student.FirstName}";
                row.Tag = student.Id;
            }
        }

        private async Task LoadCourses(long studentId)
        {
            try
            {
                var studentInCoursesTask = _enrollmentService.GetCoursesForStudentAsync(studentId);
                var courses = _courseService.GetAllAsync();
                DgvCourses.Rows.Clear();
                var studentInCourses = (await studentInCoursesTask).ToList();
                foreach (var course in await courses)
                {
                    var has = studentInCourses.FirstOrDefault(x => x.Id == course.Id);
                    var row = DgvCourses.Rows[DgvCourses.Rows.Add()];
                    row.Cells[CourseCheckBoxCol].Value = has != null;
                    row.Cells[CourseNumberCol].Value = course.Number;
                    row.Cells[CourseNameCol].Value = course.Name;
                    row.Tag = course.Id;
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Load courses");
                OnChange("Couldn't load courses for student");
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