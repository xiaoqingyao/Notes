using Notes.Events.CourseEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Domain.Core.Users.Factories
{
    public class BindCourse 
    {


        private D4LValue dv;
        private UserObj user;
        private string classId;
        private Guid dsId;
        private Guid taskId;

        private Guid pageId;

        public BindCourse(UserObj user, string classId, Guid dsId, Guid taskId)
        {
            this.user = user;
            this.classId = classId;
            this.dsId = dsId;
            this.taskId = taskId;
        }

        public Guid SecitonId => dv == null ? Guid.Empty : dv.SectionId;

        public Guid PageId => pageId;



        public void Bind()
        {



            //如果为空
            if (user.Course == null)
            {
                user.Course = new List<D4LValue>();
                this.CreateNew();
                return;
            }

            dv = user.Course.FirstOrDefault(c => c.ClassId == this.classId && c.DsId == dsId);

            if (dv == null)
            {
                this.CreateNew();
                return;
            }


            if (taskId == null)
            {
                return;
            }


            if (dv.TaskToPage != null && dv.TaskToPage.TryGetValue(taskId, out Guid pId))
            {
                this.pageId = pId;

                return;
            }

            this.CreateNewPage();

        }


        void CreateNewPage()
        {
            this.pageId = Guid.NewGuid();

            dv.AddTPItem(taskId, this.pageId);

            user.AddEvent(new CourseNotesCreated
            {
                ClassId = classId,
                Creator = user.UserId,
                DsId = dsId,
                TaskId = taskId,
                PageId = this.pageId
            });
        }


        void CreateNew()
        {
            dv = new D4LValue
            {
                ClassId = classId,
                DsId = dsId,
                SectionId = Guid.NewGuid()
            };

            user.AddEvent(new CourseNotesCreated
            {
                ClassId = classId,
                Creator = user.UserId,
                DsId = dsId,
                SectionId = dv.SectionId
            });


            //Guid pid = Guid.Empty;

            if (taskId != Guid.Empty)
            {

                this.CreateNewPage();
            }
            user.Course.Add(dv);
        }

    }
}
