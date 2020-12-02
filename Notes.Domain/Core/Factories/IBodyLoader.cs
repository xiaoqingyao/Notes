using Notes.Domain.Core.NotesBody;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Domain.Core.Factories
{
    public interface IBodyLoader
    {
        Task<bool> Del(Guid gid);
        Task<Body> LoadAsync(Guid cid);
        Task Save(Body body);
    }
}
