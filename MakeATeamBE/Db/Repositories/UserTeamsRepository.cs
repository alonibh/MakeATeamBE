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

        public void AddUserToTeam(int teamId, string userId, string userNickname)
        {
            var user = new UserTeamsDbo
            {
                TeamId = teamId,
                UserNickname = userNickname,
                UserId = userId
            };
            _dbContext.UserTeams.Add(user);

            _dbContext.SaveChanges();
        }

        public void AddUsersNicknamesToTeam(int teamId, List<string> usersNicknames)
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

        public List<string> GetUnselectedTeamPlayers(int teamId)
        {
            return _dbContext.UserTeams.Where(o => o.TeamId == teamId && o.UserId == null).Select(o => o.UserNickname).ToList();
        }

        public List<UserTeamsDbo> GetUsersInTeam(int teamId)
        {
            return _dbContext.UserTeams.Where(o => o.TeamId == teamId).ToList();
        }

        public List<int> GetUserTeams(string userId)
        {
            return _dbContext.UserTeams.Where(o => o.UserId == userId).Select(o => o.TeamId).ToList();
        }

        public bool IsTeamMember(string userId, int teamId)
        {
            return _dbContext.UserTeams.Any(o => o.UserId == userId && o.TeamId == teamId);
        }

        public void SetTeamAdmin(int teamId, string adminId, string adminNickname)
        {
            var userTeam = new UserTeamsDbo
            {
                TeamId = teamId,
                UserNickname = adminNickname,
                UserId = adminId
            };
            _dbContext.UserTeams.Add(userTeam);
            _dbContext.SaveChanges();
        }

        public void UpdateUserId(int teamId, string userId, string selectedNickname)
        {
            var user = _dbContext.UserTeams.Single(o => o.TeamId == teamId && o.UserNickname == selectedNickname);
            user.UserId = userId;
            _dbContext.SaveChanges();
        }
    }
}
