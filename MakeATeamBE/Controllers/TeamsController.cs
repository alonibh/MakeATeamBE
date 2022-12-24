using MakeATeamBE.Db.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MakeATeamBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly ILogger<TeamsController> _logger;
        private readonly ITeamRepository _teamRepository;
        private readonly IUserRepository _userRepository;

        public TeamsController(ILogger<TeamsController> logger, ITeamRepository teamRepository, IUserRepository userRepository)
        {
            _logger = logger;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public int CreateTeam(string userId, string teamName, string date)
        {
            var user = _userRepository.GetUser(userId);
            if(user == null)
            {
                _logger.LogError("Usewr not found");
                throw new Exception("Usewr not found");
            }
            var team = _teamRepository.CreateTeam(teamName, userId, user.Name, DateTime.Parse(date));

            _teamRepository.AddPlayerToTeam(team.Id, userId, user.Name);
            _userRepository.AddUserToTeam(userId, team.Id);
            return team.Id;
        }
    }
}
