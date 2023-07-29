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
        public UsersController(ILogger<UsersController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
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
    }
}
