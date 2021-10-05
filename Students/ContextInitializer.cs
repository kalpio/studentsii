namespace Students
{
    internal class ContextInitializer : Model.IContextInitializer
    {
        public string GetConnectionString()
        {
            return Properties.Settings.Default.studentsConn;
        }
    }
}
