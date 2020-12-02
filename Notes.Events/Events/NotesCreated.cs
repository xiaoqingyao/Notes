using Notes.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Events.Events
{
    public class NotesCreated : INotesEvent
    {
        public NotesEntity Notes { get; set; }


        public NotesSectionEntity Sections { get; set; }

        public NotesPageEntity Page { get; set; }
    }
}
