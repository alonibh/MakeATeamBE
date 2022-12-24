using MakeATeamBE.Db.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace MakeATeamBE.Controllers
{
    [ApiController]
    [Route("teams/{userId}")]
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
        public IEnumerable<TeamDetails> Get(string userId)
        {
            var user = _userRepository.GetUser(userId);
            if (user == null)
            {
                return new List<TeamDetails>();
            }
            var userTeams = user.Teams;
            List<TeamDetails> teams = new List<TeamDetails>();
            foreach (var teamId in userTeams)
            {
                var team = _teamRepository.GetTeam(teamId);
                teams.Add(new TeamDetails
                {
                    Id = team.Id,
                    Code= team.Code,
                    Name = team.Name,
                    Date = team.Date,
                    PlayersCount = team.Players.Count
                });
            }
            return teams;
        }
    }
}
