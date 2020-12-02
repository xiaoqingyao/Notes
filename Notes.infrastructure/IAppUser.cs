using System;
using System.Threading.Tasks;

namespace Notes.infrastructure
{
    public interface IAppUser
    {

        void SetSession(string session);


        public string Session { get; }

        public string UserId { get; }


        public string UserName { get; }


        public string Role { get; }


        public bool IsTreacher();
        Task<string> ClassIdAsync();
    }


}
