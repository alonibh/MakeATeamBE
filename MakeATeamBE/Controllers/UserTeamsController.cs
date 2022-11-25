using MakeATeamBE.Db.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace MakeATeamBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserTeamsController : ControllerBase
    {
        private readonly ILogger<UserTeamsController> _logger;
        private readonly ITeamRepository _teamRepository;
        private readonly IUserRepository _userRepository;

        public UserTeamsController(ILogger<UserTeamsController> logger, ITeamRepository teamRepository, IUserRepository userRepository)
        {
            _logger = logger;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IEnumerable<TeamDetails> Get(int userId)
        {
            var userTeams = _userRepository.GetUser(userId).Teams;
            List<TeamDetails> teams = new List<TeamDetails>();
            foreach (var teamId in userTeams)
            {
                var team = _teamRepository.GetTeam(teamId);
                teams.Add(new TeamDetails
                {
                    Id = team.Id,
                    Name = team.Name,
                    Date = team.Date,
                    PlayersCount = team.Players.Count
                });
            }
            return teams;
        }
    }
}
