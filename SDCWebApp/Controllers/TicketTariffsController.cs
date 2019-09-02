using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SDCWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketTariffsController : CustomApiController, ITicketTariffsController
    {
        public TicketTariffsController(ILogger logger) : base(logger)
        {
        }
    }
}