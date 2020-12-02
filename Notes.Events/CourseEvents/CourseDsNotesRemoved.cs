using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Events.CourseEvents
{
    public class CourseDsNotesRemoved  : INotesEvent
    {
       
        public string ClassId { get; set; }

        public Guid DsId { get; set; }

        public string Creaotor { get; set; }
    }
}
