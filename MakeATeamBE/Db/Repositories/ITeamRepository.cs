using MakeATeamBE.Db.Models;
using System;

namespace MakeATeamBE.Db.Repositories
{
    public interface ITeamRepository
    {
        public void AddPlayerToTeam(int teamId, string userId, string name);
        public TeamDbo GetTeam(int id);
        public TeamDbo GetTeamByCode(string code);
        public TeamDbo CreateTeam(string name, string adminId, string adminName, DateTime date);
        public void AddUserToSubmittedList(int teamId, string userId);
    }
}
