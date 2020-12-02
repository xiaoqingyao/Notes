using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Domain.Core.Users
{
    public interface IUserRootSvc
    {
        Task CatalogItemSubstructAsync(string userId, string catalogCode, string gradeCode);
        Task DelPageAsync(string userId, Guid sectionId, Guid pageId);
        Task DelSection(string userId, params Guid[] sectionId);
        Task<(Guid sid, Guid pid)> EntryFromD4LAsync(string userId, string classId, Guid dsId, Guid taskId);
        Task ReferenceCatalog(string userId, string catalogCode, string catalogName, string gradecode, string gradeName);
    }
}
