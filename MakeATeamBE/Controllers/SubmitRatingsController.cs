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
    public class SubmitRatingsController : ControllerBase
    {
        private readonly ILogger<SubmitRatingsController> _logger;
        private readonly IRatingRepository _ratingRepository;

        public SubmitRatingsController(ILogger<SubmitRatingsController> logger, IRatingRepository ratingRepository)
        {
            _logger = logger;
            _ratingRepository = ratingRepository;

        }

        [HttpPost]
        public void SubmitRatings(int userId, List<UserRating> ratings)
        {
            _ratingRepository.SetUserRatings(userId, ratings);
        }
    }
}
