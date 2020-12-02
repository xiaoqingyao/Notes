using LinqToDB.DataProvider.SapHana;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Events.CourseEvents
{
    public class CourseTaskPageRemoved : INotesEvent
    {
        public Guid PageId { get; set; }

        public string Creator { get; set; }

    }
}
