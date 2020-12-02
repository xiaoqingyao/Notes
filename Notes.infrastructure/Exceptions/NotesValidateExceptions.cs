using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.infrastructure.Exceptions
{
    public class NotesValidateExceptions : NotesException
    {
        public NotesValidateExceptions(string message) : base(message)
        {
        }
    }
}
