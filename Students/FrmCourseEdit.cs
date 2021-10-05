using System;
using System.Windows.Forms;
using Students.Model.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Students.Model;
using Students.Model.Entities;
using Students.Model.Exceptions;

namespace Students
{
    public partial class FrmCourseEdit : Form
    {
        private readonly ICourseService _courseService;
        private readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
        public event ChangeEventHandler Change;
        private readonly long _id;
        private Course _course;

        protected virtual void OnChange(string message)
        {
            Change?.Invoke(new ChangeEventArgs(message));
        }

        public FrmCourseEdit(long id = 0)
        {
            InitializeComponent();
            _courseService = Model.Services.Services.ServiceProvider.GetService<ICourseService>();
            _id = id;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            var result = await SaveDataAsync();
            DialogResult = result ? DialogResult.OK : DialogResult.Cancel;
            Close();
        }

        private async Task<bool> SaveDataAsync()
        {
            PrepareModelToSave();

            var saveTask = PrepareSaveTask();
            var result = false;
            try
            {
                result = await saveTask;
                OnChange("Course successfully added");
            }
            catch (ArgumentNullException e)
            {
                _logger.Error(e, "Save course to database: course cannot be empty");
                OnChange("Fill course data");
            }
            catch (RecordExistsException e)
            {
                _logger.Error(e, "Save course to database: record already exists");
                OnChange("Course with that number already exits");
            }
            catch (Exception e)
            {
                _logger.Error(e, "Save course to database");
                OnChange("An error occurred while saving data");
            }

            return result;
        }

        private void PrepareModelToSave()
        {
            if (_id == 0)
            {
                _course = new Course
                {
                    Number = Convert.ToInt64(txtNumber.Value),
                    Name = txtName.Text
                };
            }
            else
            {
                _course.Number = Convert.ToInt64(txtNumber.Value);
                _course.Name = txtName.Text;
            }
        }

        private Task<bool> PrepareSaveTask()
        {
            return _id == 0 ? _courseService.SaveAsync(_course) : _courseService.UpdateAsync(_course);
        }

        private async void FrmCourseEdit_LoadAsync(object sender, EventArgs e)
        {
            if (_id == 0)
                _ = LoadNextIdAsync().ConfigureAwait(false);
            else
                await LoadCourse();
        }

        private async Task LoadNextIdAsync()
        {
            try
            {
                var nextId = await _courseService.GetLastNumberAsync();
                txtNumber.Value = Convert.ToDecimal(nextId + 1);
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }

        private async Task LoadCourse()
        {
            if (_id <= 0)
                return;

            _course = await _courseService.GetByIdAsync(_id);
            if (_course == null)
                return;

            txtNumber.Value = Convert.ToDecimal(_course.Number);
            txtName.Text = _course.Name;
        }
    }
}