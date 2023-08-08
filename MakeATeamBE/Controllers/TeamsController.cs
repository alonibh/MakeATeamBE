using MakeATeamBE.Db.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MakeATeamBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly ILogger<TeamsController> _logger;
        private readonly ITeamRepository _teamRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserTeamsRepository _userTteamsRepository;

        public TeamsController(ILogger<TeamsController> logger, ITeamRepository teamRepository, IUserRepository userRepository, IUserTeamsRepository userTteamsRepository)
        {
            _logger = logger;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _userTteamsRepository = userTteamsRepository;
        }

        [HttpPost]
        public int CreateTeam(string userId, string teamName, string date)
        {
            var admin = _userRepository.GetUser(userId);
            if (admin == null)
            {
                _logger.LogError("User not found");
                throw new Exception("User not found");
            }

            // TODO not always this format
            string format = "d.M.yyyy, H:mm:ss";
            DateTime parsedDate = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
            var team = _teamRepository.CreateTeam(teamName, userId, admin.Name, parsedDate);

            _userTteamsRepository.AddUsersToTeam(team.Id, new List<string> { userId });
            return team.Id;
        }
    }
}
