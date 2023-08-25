using MakeATeamBE.Db.Models;
using System;
using System.Collections.Generic;

namespace MakeATeamBE.Db.Repositories
{
    public interface ITeamRepository
    {
        public TeamDbo GetTeam(int id);
        public TeamDbo GetTeamByCode(string code);
        public TeamDbo CreateTeam(string name, string adminId, string adminName, DateTime date);
        public List<string> GetTeamPlayers(int teamId);
    }
}
