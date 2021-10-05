using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Students.Model;

namespace Students
{
    public partial class FrmStudents : Form
    {
        private readonly Model.Interfaces.IStudentService _studentService;
        public event ChangeEventHandler Change;

        private void OnChange(string message)
        {
            Change?.Invoke(new ChangeEventArgs(message));
        }

        private void OnChange(ChangeEventArgs e)
        {
            OnChange(e.Message);
        }

        public FrmStudents()
        {
            InitializeComponent();
            _studentService = Model.Services.Services.ServiceProvider.GetService<Model.Interfaces.IStudentService>();
        }

        private async Task FillDataView()
        {
            dgvStudent.DataSource = await _studentService.GetAllAsync();
        }

        private async void FrmStudents_Load(object sender, EventArgs e)
        {
            InitializeButtons();
            await FillDataView();
            DGVConfigurator.ConfigureDataGridView(ref dgvStudent);
        }

        private void InitializeButtons()
        {
            btnAdd.Image = Properties.Resources.plus;
        }

        private async void BtnAdd_ClickAsync(object sender, EventArgs e)
        {
            var f = new FrmStudentEdit
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