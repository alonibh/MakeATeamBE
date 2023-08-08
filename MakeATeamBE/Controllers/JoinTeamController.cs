using MakeATeamBE.Db.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly IUserTeamsRepository _userTeamsRepository;

        public JoinTeamController(ILogger<JoinTeamController> logger, ITeamRepository teamRepository, IUserRepository userRepository, IUserTeamsRepository userTeamsRepository)
        {
            _logger = logger;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _userTeamsRepository = userTeamsRepository;
        }

        [HttpPost]
        public TeamDetails JoinTeam(string userId, string teamCode)
        {
            var user = _userRepository.GetUser(userId);
            var team = _teamRepository.GetTeamByCode(teamCode);

            _userTeamsRepository.AddUserToTeam(team.Id, userId, user.Name);
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
