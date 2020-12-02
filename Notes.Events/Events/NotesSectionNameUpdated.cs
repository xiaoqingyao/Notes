using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Events.Events
{
    public class NotesSectionNameUpdated : INotesEvent
    {

        public Guid Id { get; set; }


        public Guid NotesId { get; set; }

        public string Name { get; set; }
    }
}
