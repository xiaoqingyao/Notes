using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Events.Events
{
    public class NotesNameUpdated : INotesEvent
    {
        public Guid Id { get; set; }


        public string Name { get; set; }
    }
}
