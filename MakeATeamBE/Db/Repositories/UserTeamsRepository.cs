using MakeATeamBE.Db.Models;
using System.Collections.Generic;
using System.Linq;

namespace MakeATeamBE.Db.Repositories
{
    public class UserTeamsRepository : IUserTeamsRepository
    {
        private readonly MakeATeamContext _dbContext;

        public UserTeamsRepository(MakeATeamContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUsersToTeam(int teamId, List<string> usersNicknames)
        {
            foreach (var userNickname in usersNicknames)
            {
                var user = new UserTeamsDbo
                {
                    TeamId = teamId,
                    UserNickname = userNickname,
                };
                _dbContext.UserTeams.Add(user);
            }

            _dbContext.SaveChanges();
        }

        public List<int> GetUserTeams(string userId)
        {
            return _dbContext.UserTeams.Where(o => o.UserId == userId).Select(o => o.TeamId).ToList();
        }

        public void UpdateUserId(int teamId, string userGivenName, string userId)
        {
            var user = _dbContext.UserTeams.Single(o => o.TeamId == teamId && o.UserNickname == userGivenName);
            user.UserId = userId;
            _dbContext.SaveChanges();
        }
    }
}
