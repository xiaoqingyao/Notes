using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.infrastructure.Exceptions
{
    public class Notes3PartApiException : NotesException
    {
        public Notes3PartApiException(string message) : base(message)
        {
        }
    }
}
