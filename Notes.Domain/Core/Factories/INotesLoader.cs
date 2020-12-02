using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Domain.Core.Factories
{
    public interface INotesLoader
    {

        /// <summary>
        /// 笔记本
        /// </summary>
        /// <returns></returns>
        Task<NotesObj> LoadAsync(Guid id);
        Task Save(NotesObj notes);
        Task<bool> Remove(Guid nid);
    }
}
