using Notes.Domain.DTOS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Domain.Core
{
    public interface INotesSvc
    {

        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        Task<NotesObj> CreateAsync(CreateNotesDTO dto, Guid notesId, string userName, string userId);
        Task<NotesObj> CreatePage(Guid notesId, Guid sectionId, Guid pid,string name, string content);
        Task<NotesObj> CreateSection(Guid noteId,Guid sId, string name);
        Task<NotesObj> DelNotes(Guid nid);
        Task<bool> DelPage(Guid nid, Guid sid, Guid pid);
        Task<IList<Guid>> DelSection(Guid nid, Guid sId);
        Task<NotesObj> Get(Guid nid, Guid sectionId, Guid pid);
        //Task<bool> UpdateContent(Guid pid, string content);
        Task<bool> UpdateNotesName(Guid id, string name);
        Task<bool> UpdatePageName(Guid nid, Guid sid, Guid pid, string name);
        Task<bool> UpdateSectionName(Guid notesId, Guid sid, string name);
    }
}
