using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Students.Model;
using Students.Model.Interfaces;

namespace Students
{
    public partial class FrmCourses : Form
    {
        private readonly ICourseService _courseService;
        public event ChangeEventHandler Change;

        private void OnChange(string message)
        {
            Change?.Invoke(new ChangeEventArgs(message));
        }

        private void OnChange(ChangeEventArgs e)
        {
            OnChange(e.Message);
        }

        public FrmCourses()
        {
            InitializeComponent();
            _courseService = Model.Services.Services.ServiceProvider.GetService<ICourseService>();
        }

        private async void FrmCourses_Load(object sender, EventArgs e)
        {
            InitializeControls();
            await FillDataView();
            DGVConfigurator.ConfigureDataGridView(ref dgvCourses);
        }

        private const string IdCol = "id";
        private const string NumberCol = "number";
        private const string NameCol = "name";

        private void InitializeControls()
        {
            BtnAdd.Image = Properties.Resources.plus;
            BtnEdit.Image = Properties.Resources.pencil;

            dgvCourses.Columns.AddRange(
                new DataGridViewTextBoxColumn
                {
                    Name = IdCol,
                    HeaderText = "#"
                }, new DataGridViewTextBoxColumn
                {
                    Name = NumberCol,
                    HeaderText = "Number"
                }, new DataGridViewTextBoxColumn
                {
                    Name = NameCol,
                    HeaderText = "Name"
                });
        }

        private async Task FillDataView()
        {
            dgvCourses.Rows.Clear();
            var courses = await _courseService.GetAllAsync();
            foreach (var course in courses)
            {
                var row = dgvCourses.Rows[dgvCourses.Rows.Add()];
                row.Cells[IdCol].Value = course.Id;
                row.Cells[NumberCol].Value = course.Number;
                row.Cells[NameCol].Value = course.Name;
                row.Tag = course.Id;
            }
        }

        private async void BtnAdd_ClickAsync(object sender, EventArgs e)
        {
            var f = new FrmCourseEdit
            {
                WindowState = FormWindowState.Normal,
                StartPosition = FormStartPosition.CenterParent
            };
            f.Change += OnChange;
            if (f.ShowDialog() == DialogResult.OK)
            {
                await FillDataView();
            }
        }

        private async void BtnEdit_ClickAsync(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count == 0)
                return;

            var row = dgvCourses.SelectedRows[0];
            var id = (long)row.Tag;
            var f = new FrmCourseEdit(id)
            {
                WindowState = FormWindowState.Normal,
                StartPosition = FormStartPosition.CenterParent
            };
            f.Change += OnChange;
            if (f.ShowDialog() == DialogResult.OK)
            {
                await FillDataView();
            }
        }
    }
}