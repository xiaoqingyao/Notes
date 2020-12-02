using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Events.Events
{
    public class NotesPageCreated : INotesEvent
    {
        public Guid NotesId { get; set; }

        public Guid SectionId { get; set; }

        public Guid PageId { get; set; }

        public string ClassId { get; set; }

        public string Name { get; set; }

        public string Creator { get; set; }
    }
}
