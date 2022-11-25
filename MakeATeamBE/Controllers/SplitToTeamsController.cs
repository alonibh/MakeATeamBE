using MakeATeamBE.Db.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace MakeATeamBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SplitToTeamsController : ControllerBase
    {
        private readonly ILogger<SplitToTeamsController> _logger;
        private readonly ITeamRepository _teamRepository;
        private readonly IRatingRepository _ratingRepository;

        public SplitToTeamsController(ILogger<SplitToTeamsController> logger, ITeamRepository teamRepository, IRatingRepository ratingRepository)
        {
            _logger = logger;
            _teamRepository = teamRepository;
            _ratingRepository = ratingRepository;
        }

        [HttpPost]
        public TeamPlayers[] SplitToTeams(int teamId, int numberOfTeams)
        {
            var team = _teamRepository.GetTeam(teamId);

            Dictionary<int, (int RatingsSum, int Count)> totalRatings = new Dictionary<int, (int RatingsSum, int Count)>();
            foreach (var player in team.Players)
            {
                if (!totalRatings.ContainsKey(player.Id))
                {
                    totalRatings.Add(player.Id, (0, 0));
                }
                var userRatings = _ratingRepository.GetUserRatings(player.Id);
                foreach (var rating in userRatings)
                {
                    if (team.Players.Any(o => o.Id == rating.RatingSubjectId))
                    {
                        totalRatings.TryGetValue(rating.RatingSubjectId, out var totalRating);
                        totalRatings[rating.RatingSubjectId] = (totalRating.RatingsSum + rating.Rating, totalRating.Count + 1);
                    }
                }
            }
            List<(int Id, double AverageRating)> averageRatings = new List<(int Id, double AverageRating)>();
            foreach (var item in totalRatings)
            {
                if (item.Value.Count == 0)
                    averageRatings.Add((item.Key, 0));
                else
                    averageRatings.Add((item.Key, (double)item.Value.RatingsSum / item.Value.Count));
            }

            averageRatings = averageRatings.OrderByDescending(o => o.AverageRating).ToList();

            TeamPlayers[] teamsPlayers = new TeamPlayers[numberOfTeams];
            for (int i = 0; i < numberOfTeams; i++)
            {
                teamsPlayers[i] = new TeamPlayers();
                teamsPlayers[i].Names = new List<string>();
            }

            for (int i = 0; i < averageRatings.Count; i++)
            {
                string playerName = team.Players.Single(o => o.Id == averageRatings[i].Id).Name;
                teamsPlayers[i % numberOfTeams].Names.Add(playerName);
            }

            return teamsPlayers;
        }
    }
}
