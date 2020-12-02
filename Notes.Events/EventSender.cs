using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Events
{
    public class EventSender : IEventSender
    {


        private readonly ICapPublisher _capBus;

        public EventSender(ICapPublisher capBus)
        {
            _capBus = capBus;
        }

        public Task SendAsync<T>(T data, string name = null) where T : INotesEvent
        {

            if (String.IsNullOrEmpty(name))
            {

                name = typeof(T).Name;

            }


            return this._capBus.PublishAsync(name, data);
        }


        //public Task SendBatchAsync<T>(IList<T> data, string name) 
        //    where T : INotesEvent
        //{
        //    if (data == null || data.Count == 0)
        //    {
        //        return;
        //    }
        //    foreach (var item in data)
        //    {
        //        this.SendAsync(item, name);
        //    }
        //}

    }
}
