using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Domain.Core.Users
{
    public class D4LValue
    {

        public string ClassId { get; set; }

        public Guid DsId { get; set; }


        public Guid SectionId { get; set; }


        

        /// <summary>
        /// Key Taskid value pageId
        /// </summary>
        public IDictionary<Guid,Guid> TaskToPage { get; set; }

        
        public bool AddTPItem(Guid taskId, Guid pageId)
        {
            if (TaskToPage == null)
            {
                TaskToPage = new Dictionary<Guid, Guid>(); 
            }
            return TaskToPage.TryAdd(taskId, pageId);
        }

        public bool RemoveTask(Guid pageId)
        {
            if (TaskToPage == null)
            {
                return false;
            }
            var key = this.TaskToPage.SingleOrDefault(s => s.Value == pageId).Key;
            return this.TaskToPage.Remove(key);
        }


        public override bool Equals(object obj)
        {
            if (obj is D4LValue val)
            {
                if (val.ClassId == this.ClassId || this.SectionId == val.SectionId)
                {
                    return true;
                }
            }

            return false;
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), this.ClassId.GetHashCode(), this.SectionId.GetHashCode());
        }
    }
    
}
