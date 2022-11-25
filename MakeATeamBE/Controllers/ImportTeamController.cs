using MakeATeamBE.Db.Repositories;
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
        private readonly ILogger<ImportTeamController> _logger;
        private readonly ITeamRepository _teamRepository;
        private readonly IUserRepository _userRepository;

        public ImportTeamController(ILogger<ImportTeamController> logger, ITeamRepository teamRepository, IUserRepository userRepository)
        {
            _logger = logger;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public TeamDetails ImportTeam(int userId, string teamCode)
        {
            var user = _userRepository.GetUser(userId);
            var team = _teamRepository.GetTeamByCode(teamCode);
            if (user.Teams.Any(o => o == team.Id))
            {
                return null;
            }
            _teamRepository.AddPlayerToTeam(team.Id, userId, user.Name);
            _userRepository.AddUserToTeam(userId, team.Id);
            return new TeamDetails
            {
                Id = team.Id,
                Date = team.Date,
                Name = team.Name,
                PlayersCount = team.Players.Count+1
            };
        }
    }
}
