using MakeATeamBE.Db.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MakeATeamBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly ITeamRepository _teamRepository;

        public UsersController(ILogger<UsersController> logger, IUserRepository userRepository, ITeamRepository teamRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _teamRepository = teamRepository;
        }

        [HttpPost]
        public bool AddUser(string userId, string name)
        {
            return _userRepository.AddUser(userId, name);

        }

        [HttpPatch]
        public void UpdateUser(string userId, string name)
        {
            _userRepository.UpdateUser(userId, name);
        }

        [Route("{userId}")]
        [HttpGet]
        public string GetUserName(string userId)
        {
            return _userRepository.GetUser(userId).Name;
        }

        [Route("{userId}/teamMember")]
        [HttpGet]
        public string GetIsTeamMember(string userId, string teamCode)
        {
            return _userRepository.GetUser(userId).Name;
        }
    }
}
