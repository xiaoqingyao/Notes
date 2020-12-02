using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Events.Events
{
    public class NotesSectionDeleted : INotesEvent
    {
        public Guid SectionId { get; set; }


        public Guid NotesId { get; set; }
    }

}
