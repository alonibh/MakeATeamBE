using MakeATeamBE.Db.Models;
using System.Collections.Generic;

namespace MakeATeamBE.Db.Repositories
{
    public interface IUserTeamsRepository
    {
        void AddUserToTeam(int teamId, string userId, string userNickname);
        public void AddUsersNicknamesToTeam(int teamId, List<string> usersNicknames);
        List<string> GetUnselectedTeamPlayers(int teamId);
        List<UserTeamsDbo> GetUsersInTeam(int teamId);
        public List<int> GetUserTeams(string userId);
        bool IsTeamMember(string userId, int teamId);
        void SetTeamAdmin(int teamId, string adminId, string adminNickname);
        public void UpdateUserId(int teamId, string userId, string selectedNickname);
    }
}
