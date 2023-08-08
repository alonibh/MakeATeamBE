using System.Collections.Generic;

namespace MakeATeamBE.Db.Repositories
{
    public interface IUserTeamsRepository
    {
        public void AddUsersToTeam(int teamId, List<string> usersNicknames);
        public List<int> GetUserTeams(string userId);
        public void UpdateUserId(int teamId, string userNickname, string userId);
    }
}
