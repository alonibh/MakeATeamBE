using MakeATeamBE.Db.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MakeATeamBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly ILogger<TeamsController> _logger;
        private readonly ITeamRepository _teamRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserTeamsRepository _userTeamsRepository;
        private readonly IRatingRepository _ratingRepository;

        public TeamsController(ILogger<TeamsController> logger, ITeamRepository teamRepository, IUserRepository userRepository, IUserTeamsRepository userTteamsRepository, IRatingRepository ratingRepository)
        {
            _logger = logger;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _userTeamsRepository = userTteamsRepository;
            _ratingRepository = ratingRepository;
        }

        [HttpPost]
        public int CreateTeam(string adminId, string teamName, string date)
        {
            var admin = _userRepository.GetUser(adminId);
            if (admin == null)
            {
                _logger.LogError("User not found");
                throw new Exception("User not found");
            }

            // TODO not always this format
            string format = "d.M.yyyy, H:mm:ss";
            DateTime parsedDate = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
            var team = _teamRepository.CreateTeam(teamName, admin.Id, admin.Name, parsedDate);

            _userTeamsRepository.AddUserToTeam(team.Id, admin.Id, admin.Name);
            return team.Id;
        }

        [Route("{teamId}/addPlayers")]
        [HttpPost]
        public void AddPlayersToTeam(string teamId, [FromBody] List<string> playersNicknames)
        {
            var team = _teamRepository.GetTeam(int.Parse(teamId));
            if (team == null)
            {
                _logger.LogError("Team not found");
                throw new Exception("Team not found");
            }

            _userTeamsRepository.AddUsersNicknamesToTeam(team.Id, playersNicknames);
        }

        [Route("{teamId}/unselectedPlayers")]
        [HttpGet]
        public UnselectedTeamPlayersResponse GetUnselectedTeamPlayers(string teamId)
        {
            var unselectedTeamPlayers = _userTeamsRepository.GetUnselectedTeamPlayers(int.Parse(teamId));
            string teamName = _teamRepository.GetTeam(int.Parse(teamId)).Name;

            return new UnselectedTeamPlayersResponse
            {
                TeamName = teamName,
                Players = unselectedTeamPlayers
            };
        }

        [Route("{teamId}/name")]
        [HttpGet]
        public string GetTeamName(string teamId)
        {
            return _teamRepository.GetTeam(int.Parse(teamId)).Name;
        }

        [Route("{userId}")]
        [HttpGet]
        public IEnumerable<TeamDetails> GetUserTeams(string userId)
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

        [Route("{teamId}/split")]
        [HttpPost]
        public TeamPlayers[] SplitToTeams(int teamId, int numberOfTeams)
        {
            var team = _teamRepository.GetTeam(teamId);
            var playersInTeam = _userTeamsRepository.GetUsersInTeam(teamId);

            Dictionary<string, (int RatingsSum, int Count)> totalRatings = new Dictionary<string, (int RatingsSum, int Count)>();
            foreach (var player in playersInTeam)
            {
                if (!totalRatings.ContainsKey(player.UserNickname))
                {
                    totalRatings.Add(player.UserNickname, (0, 0));
                }

                var userRatings = _ratingRepository.GetUserRatings(player.UserId);
                foreach (var rating in userRatings)
                {
                    if (playersInTeam.Any(o => o.UserNickname == rating.RatingSubjectNickname))
                    {
                        totalRatings.TryGetValue(rating.RatingSubjectNickname, out var totalRating);
                        totalRatings[rating.RatingSubjectNickname] = (totalRating.RatingsSum + rating.Rating, totalRating.Count + 1);
                    }
                }
            }

            List<(string Nickname, double AverageRating)> averageRatings = new List<(string Nickname, double AverageRating)>();
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
                // Catan based order logic
                int teamIndex = 0;
                if (i % (numberOfTeams * 2) < numberOfTeams)
                    teamIndex = i % numberOfTeams;
                else
                    teamIndex = numberOfTeams - 1 - (i % numberOfTeams);


                teamsPlayers[teamIndex].Names.Add(averageRatings[i].Nickname);
            }

            return teamsPlayers;
        }


        [Route("{teamId}/join")]
        [HttpPost]
        public void JoinTeam(int teamId, string userId, string selectedNickname)
        {
            _userTeamsRepository.UpdateUserId(teamId, userId,selectedNickname);
        }
    }
}
