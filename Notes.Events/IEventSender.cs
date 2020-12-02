using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Events
{
    public interface IEventSender
    {
        Task SendAsync<T>(T data, string name = null) where T : INotesEvent;




    }
}
