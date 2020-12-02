using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.infrastructure.Exceptions
{
    public class NotesNotFoundException : NotesException
    {
        public NotesNotFoundException(string message) : base(message)
        {
        }
    }
}
