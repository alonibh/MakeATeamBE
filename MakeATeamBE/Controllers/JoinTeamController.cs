using MakeATeamBE.Db.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace MakeATeamBE.Controllers
{
    [ApiController]
    [Route("teams/join")]
    public class JoinTeamController : ControllerBase
    {
        private readonly ILogger<JoinTeamController> _logger;
        private readonly ITeamRepository _teamRepository;
        private readonly IUserRepository _userRepository;

        public JoinTeamController(ILogger<JoinTeamController> logger, ITeamRepository teamRepository, IUserRepository userRepository)
        {
            _logger = logger;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public TeamDetails JoinTeam(string userId, string teamCode)
        {
            var user = _userRepository.GetUser(userId);
            var team = _teamRepository.GetTeamByCode(teamCode);
            if (user.Teams.Any(o => o == team.Id))
            {
                throw new Exception($"Player {user.Id} already a part of team {team.Id}");
            }
            _teamRepository.AddPlayerToTeam(team.Id, userId, user.Name);
            _userRepository.AddUserToTeam(userId, team.Id);
            return new TeamDetails
            {
                Id = team.Id,
                Code = team.Code,
                Date = team.Date,
                Name = team.Name,
                PlayersCount = team.Players.Count + 1
            };
        }
    }
}
