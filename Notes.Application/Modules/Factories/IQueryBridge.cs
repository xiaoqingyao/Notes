using Notes.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Application.Modules.Factories
{
    public interface IQueryBridge
    {
        Task<NotesObj> GetDesignNotes(string classId, string creator, string catalogCode, string gradeCode, Guid dsId, Guid taskId);
    }
}
