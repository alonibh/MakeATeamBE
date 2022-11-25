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
    public class UserTeamSettingsController : ControllerBase
    {
        private readonly ILogger<UserTeamSettingsController> _logger;
        private readonly ITeamRepository _teamRepository;
        private readonly IRatingRepository _ratingRepository;

        public UserTeamSettingsController(ILogger<UserTeamSettingsController> logger, ITeamRepository teamRepository, IRatingRepository ratingRepository)
        {
            _logger = logger;
            _teamRepository = teamRepository;
            _ratingRepository = ratingRepository;
        }

        [HttpGet]
        public UserTeamSettings Get(int userId, int teamId)
        {
            var team = _teamRepository.GetTeam(teamId);
            var playersId = team.Players.Select(o => o.Id);
            var userRatings = _ratingRepository.GetUserRatings(userId);
            var relevantUserRatings = userRatings.Where(o => playersId.Contains(o.RatingSubjectId));

            List<UserRating> ratings = new List<UserRating>();
            foreach (var player in team.Players)
            {
                if (player.Id != userId)
                {
                    ratings.Add(new UserRating
                    {
                        UserId = player.Id
                    });
                }
            }

            foreach (var userRating in relevantUserRatings)
            {
                ratings.Single(o => o.UserId == userRating.RatingSubjectId).Rating = userRating.Rating;

            }

            return new UserTeamSettings
            {
                Name = team.Name,
                Ratings = ratings,
                IsUserAdminOfTeam = team.AdminId == userId,
            };
        }
    }
}
