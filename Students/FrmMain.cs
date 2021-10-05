using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Students.Model;

namespace Students
{
    public partial class FrmMain : Form
    {
        private readonly IStudentInitializer _studentInitializer;

        public FrmMain()
        {
            _studentInitializer = Model.Services.Services.ServiceProvider.GetService<IStudentInitializer>();
            _statusTokenSource = new CancellationTokenSource();
            InitializeComponent();
            messagePanel1.Visible = false;
        }

        private async Task InitializeDatabaseAsync()
        {
            _studentInitializer.Change += OnChange;

            await _studentInitializer.CreateDatabaseAsync();
            await _studentInitializer.InsertTestDataAsync();
        }

        private async void OnChange(ChangeEventArgs args)
        {
            await ChangeStatusMessage(args.Message);
        }

        private void MnuClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private readonly CancellationTokenSource _statusTokenSource;

        public async Task ChangeStatusMessage(string message)
        {
            if (!messagePanel1.Visible)
                messagePanel1.Visible = true;

            messagePanel1.Message = message;
            await HideStatusMessage();
        }

        private async Task HideStatusMessage()
        {
            try
            {
                await Task.Delay(3000, _statusTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                return;
            }

            messagePanel1.Visible = false;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            InitializeDatabaseAsync().ConfigureAwait(false);
        }

        private void MnuStudentList_Click(object sender, EventArgs e)
        {
            var f = new FrmStudents
            {
                MdiParent = this,
                WindowState = FormWindowState.Maximized
            };
            f.Change += OnChange;
            f.Show();
        }

        private void MnuCourseAdd_Click(object sender, EventArgs e)
        {
            var f = new FrmCourseEdit
            {
                StartPosition = FormStartPosition.CenterParent
            };
            f.Change += OnChange;
            if (f.ShowDialog() != DialogResult.OK) return;
            var fCourse = new FrmCourses
            {
                MdiParent = this,
                WindowState = FormWindowState.Maximized
            };
            fCourse.Change += OnChange;
            fCourse.Show();
        }

        private void MnuCourseList_Click(object sender, EventArgs e)
        {
            var f = new FrmCourses
            {
                MdiParent = this,
                WindowState = FormWindowState.Maximized
            };
            f.Change += OnChange;
            f.Show();
        }

        private void mnuAddStudentToCourse_Click(object sender, EventArgs e)
        {
            var f = new FrmEnrollmentEdit
            {
                WindowState = FormWindowState.Normal,
                StartPosition = FormStartPosition.CenterParent
            };
            f.Change += OnChange;
            if (f.ShowDialog() != DialogResult.OK)
                return;

            var fEnrollments = new FrmEnrollments
            {
                MdiParent = this,
                WindowState = FormWindowState.Maximized
            };
            fEnrollments.Change += OnChange;
            fEnrollments.Show();
        }

        private void mnuEnrollmentsList_Click(object sender, EventArgs e)
        {
            var f = new FrmEnrollments
            {
                MdiParent = this,
                WindowState = FormWindowState.Maximized
            };
            f.Change += OnChange;
            f.Show();
        }

        private void mnuEnrollmentsCourses_Click(object sender, EventArgs e)
        {
            var f = new FrmEnrollmentsCourses
            {
                MdiParent = this,
                WindowState = FormWindowState.Maximized
            };
            f.Change += OnChange;
            f.Show();
        }
    }
}