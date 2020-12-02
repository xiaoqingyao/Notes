using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Events.Events
{
    public class NotesDeleted : INotesEvent
    {
        public Guid NotesId { get; set; }
    }
}
