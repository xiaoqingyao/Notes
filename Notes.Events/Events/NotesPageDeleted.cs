using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Events.Events
{
    public class NotesPageDeleted : INotesEvent
    {
        public Guid PageId { get; set; }
        

        public Guid NotesId { get; set; }
    }
}
