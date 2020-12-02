using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Events.Events
{
    public class NotesSectionCreated : INotesEvent
    {
        public Guid NotesId { get; set; }


        public Guid SectionId { get; set; }


        public string Name { get; set; }
    }
}
