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
        private readonly IRatingRepository _ratingRepository;

        public RatingsController(ILogger<RatingsController> logger, IRatingRepository ratingRepository)
        {
            _logger = logger;
            _ratingRepository = ratingRepository;

        }

        [HttpPost]
        public void SubmitRatings(string userId, List<UserRating> ratings)
        {
            _ratingRepository.SetUserRatings(userId, ratings);
        }
    }
}
