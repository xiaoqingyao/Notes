using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Notes.infrastructure.Exceptions
{
    public class NotesException : Exception
    {
        public NotesException(string message) : base(message)
        {
        }
    }
}
