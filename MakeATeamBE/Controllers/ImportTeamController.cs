using MakeATeamBE.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeATeamBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImportTeamController : ControllerBase
    {
        private static readonly TeamDetails teamDetails = new TeamDetails
        {
            Id = 4,
            Name = "Imported Team",
            Date = DateTime.Now,
            PlayersCount = 4
        };

        private readonly ILogger<ImportTeamController> _logger;

        public ImportTeamController(ILogger<ImportTeamController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public TeamDetails ImportTeam(string teamCode)
        {
            return teamDetails;
        }
    }
}
