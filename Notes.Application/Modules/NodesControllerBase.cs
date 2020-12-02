using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Notes.Application.Modules
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class NodesControllerBase : ControllerBase
    {
    }
}
