using Notes.Domain.Core.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Domain.Core.Factories
{
    public interface IUserLoader
    {
        bool IsFromDb { get; }

        Task<UserObj> Load(string userId);
        Task Save(UserObj u);
    }
}
