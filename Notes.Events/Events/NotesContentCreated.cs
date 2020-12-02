using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Events.Events
{
    public class NotesContentCreated : INotesEvent
    {
        public Guid Id { get; set; }

        public Guid Pid { get; set; }

        public Guid SecitonId { get; set; }


        public Guid NotesId { get; set; }


        public string Content { get; set; }
    }
}
