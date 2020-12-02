using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Notes.Events.Events
{
    public class NotesContentUpdated : INotesEvent
    {
        public Guid PId { get; set; }

        public string Content { get; set; }
    }
}
