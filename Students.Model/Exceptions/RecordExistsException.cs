using System;

namespace Students.Model.Exceptions
{
    public class RecordExistsException : Exception
    {
        public RecordExistsException(string message) : base(message)
        {
        }
    }
}