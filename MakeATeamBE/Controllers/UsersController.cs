using MakeATeamBE.Db.Repositories;
using MakeATeamBE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace MakeATeamBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IUserTeamsRepository _userTeamsRepository;
        private readonly ITeamRepository _teamRepository;

        public UsersController(ILogger<UsersController> logger, IUserRepository userRepository, ITeamRepository teamRepository, IUserTeamsRepository userTeamsRepository, IRatingRepository ratingRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _teamRepository = teamRepository;
            _userTeamsRepository = userTeamsRepository;
            _ratingRepository = ratingRepository;
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
        public IsTeamMemberResponse IsTeamMember(string userId, string teamCode)
        {
            var team = _teamRepository.GetTeamByCode(teamCode);
            bool isTeamMember = _userTeamsRepository.IsTeamMember(userId, team.Id);
            return new IsTeamMemberResponse { IsTeamMember = isTeamMember, TeamId = team.Id };
        }

        [Route("{userId}/teamSettings")]
        [HttpGet]
        public UserTeamSettings GetTeamSettings(string userId, int teamId)
        {
            var playersInTeam = _userTeamsRepository.GetUsersInTeam(teamId);
            var userRatings = _ratingRepository.GetUserRatings(userId);

            List<UserRating> ratings = new List<UserRating>();
            foreach (var player in playersInTeam)
            {
                if (player.UserId != userId)
                {
                    var ratingToAdd = new UserRating
                    {
                        SubjectNickname = player.UserNickname
                    };

                    var rating = userRatings.Where(o => o.RatingSubjectNickname == player.UserNickname).SingleOrDefault();
                    if (rating != null)
                    {
                        ratingToAdd.Rating = rating.Rating;
                    }

                    ratings.Add(ratingToAdd);
                }
            }

            var team = _teamRepository.GetTeam(teamId);
            return new UserTeamSettings
            {
                Name = team.Name,
                Ratings = ratings,
                IsUserAdminOfTeam = team.AdminId == userId,
            };
        }
    }
}
