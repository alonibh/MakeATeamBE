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
        private readonly IUserTeamsRepository _userTeamsRepository;

        public UserTeamsController(ILogger<UserTeamsController> logger, ITeamRepository teamRepository, IUserTeamsRepository userTeamsRepository)
        {
            _logger = logger;
            _teamRepository = teamRepository;
            _userTeamsRepository = userTeamsRepository;
        }

        [HttpGet]
        public IEnumerable<TeamDetails> Get(string userId)
        {
            var userTeams = _userTeamsRepository.GetUserTeams(userId);
            List<TeamDetails> teams = new List<TeamDetails>();
            foreach (var teamId in userTeams)
            {
                var team = _teamRepository.GetTeam(teamId);
                teams.Add(new TeamDetails
                {
                    Id = team.Id,
                    Code = team.Code,
                    Name = team.Name,
                    Date = team.Date,
                });
            }
            return teams;
        }
    }
}
