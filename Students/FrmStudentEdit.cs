using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Students.Model;
using Students.Model.Entities;
using Students.Model.Interfaces;

namespace Students
{
    public partial class FrmStudentEdit : Form
    {
        private readonly IStudentService _studentService;
        private readonly ILogger _logger;
        public event ChangeEventHandler Change;

        private void OnChange(string message)
        {
            Change?.Invoke(new ChangeEventArgs(message));
        }

        public FrmStudentEdit()
        {
            InitializeComponent();
            _studentService = Model.Services.Services.ServiceProvider.GetService<IStudentService>();
            _logger = LogManager.GetCurrentClassLogger();
        }

        private void FrmStudentEdit_Load(object sender, EventArgs e)
        {
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private async void BtnSave_ClickAsync(object sender, EventArgs e)
        {
            await SaveData();
            DialogResult = DialogResult.OK;
            Close();
        }

        private async Task SaveData()
        {
            try
            {
                var date = new DateTime(dtDateOfBirth.Value.Year, dtDateOfBirth.Value.Month, dtDateOfBirth.Value.Day);
                var model = new Student
                {
                    FirstName = txtFirstname.Text,
                    LastName = txtLastName.Text,
                    DateOfBirth = date,
                    PersonalId = txtPersonalID.Text
                };
                var result = await _studentService.SaveAsync(model);
                if (result)
                    OnChange("Student successfully saved");
            }
            catch (Exception e)
            {
                _logger.Error(e, "Save student data");
                OnChange("Couldn't save student");
            }
        }
    }
}