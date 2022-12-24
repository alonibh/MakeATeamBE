using MakeATeamBE.Db.Repositories;
using MakeATeamBE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace MakeATeamBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingsController : ControllerBase
    {
        private readonly ILogger<RatingsController> _logger;
        private readonly ITeamRepository _teamRepository;
        private readonly IRatingRepository _ratingRepository;

        public RatingsController(ILogger<RatingsController> logger, ITeamRepository teamRepository, IRatingRepository ratingRepository)
        {
            _logger = logger;
            _teamRepository = teamRepository;
            _ratingRepository = ratingRepository;

        }

        [HttpPost]
        public void SetRatings(string userId, int teamId, List<UserRating> ratings)
        {
            _ratingRepository.SetUserRatings(userId, ratings);
            _teamRepository.AddUserToSubmittedList(teamId, userId);
        }
    }
}
