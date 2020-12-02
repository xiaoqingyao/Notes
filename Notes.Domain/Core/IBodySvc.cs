using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Domain.Core
{
    public interface IBodySvc
    {
        Task<bool> Create(Guid notesId, Guid sectionId, Guid pid, string content);
        Task<bool> Del(params Guid[] pid);
        Task<string> Get(Guid pid);
        Task<bool> Update(Guid pid, string content);
    }
}
