using Notes.Domain.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Application.Modules.Factories
{
    public interface IEntry
    {
        Task<string> DoAsync(D4LDTO dto);
    }
}
