using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Events.CourseEvents
{
    public class CourseNotesCreated : INotesEvent
    {
        public string ClassId { get; set; }

        public string Creator { get; set; }


        public Guid DsId { get; set; }

        public Guid SectionId { get; set; }


        public Guid TaskId { get; set; }


        public Guid PageId { get; set; }
    }


}
