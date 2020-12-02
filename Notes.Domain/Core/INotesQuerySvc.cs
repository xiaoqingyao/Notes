using Notes.Domain.Core.Users;
using Notes.Domain.VO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Domain.Core
{
    public interface INotesQuerySvc
    {
        Task<CatalogVO> CatalogEnableFilterAsync(string userId, string termIdx, string subjectIdx);
        Task<List<ClassPageCountVO>> CountPagesAsync(Guid dsId, string classid);
        Task<NotesVO> Get(Guid id, Guid sectionid, Guid pageId);
        Task<Guid> GetBySubject(string catalogCode, string gradeCode, string creator, string classId);
        Task<Pagnation<NotesVOItem>> ListAll(string catalogCode, string gradeCode, string createor, int pageSize, int pageIndex);
    }
}
