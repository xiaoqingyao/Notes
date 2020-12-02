using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Events.Events
{
    public class NotesContentDeleted: INotesEvent
    {
        public Guid PId { get; set; }
    }
}
