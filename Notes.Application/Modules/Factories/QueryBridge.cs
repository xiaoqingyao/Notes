using Notes.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Notes.Application.Modules.Factories
{
    public class QueryBridge : IQueryBridge
    {


        private readonly INotesQuerySvc _query;
        private readonly INotesSvc _nsvc;

        public QueryBridge(INotesQuerySvc query, INotesSvc nsvc)
        {
            _query = query;

            _nsvc = nsvc;

        }

        public async Task<NotesObj> GetDesignNotes(string classId, string creator, string catalogCode, string gradeCode,
                                                    Guid dsId
                                                   ,Guid taskId)
        {
            var notesid = await _query.GetBySubject(catalogCode, gradeCode, creator, classId);
            if (notesid == Guid.Empty)
            {
                return null;
            }

            var notes = await this._nsvc.Get(notesid, dsId, taskId);

            return notes;
        }

    }
}
