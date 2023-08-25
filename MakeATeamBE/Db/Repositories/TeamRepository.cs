using MakeATeamBE.Db.Models;
using MakeATeamBE.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakeATeamBE.Db.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly MakeATeamContext _dbContext;

        public TeamRepository(MakeATeamContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TeamDbo CreateTeam(string name, string adminId, string adminName, DateTime date)
        {
            var team = new TeamDbo
            {
                Name = name,
                AdminId = adminId,
                Code = CommonUtils.CreateRandomTeamCode(),
                Date = date.ToUniversalTime(), // TODO support UTC in FE
            };
            _dbContext.Teams.Add(team);
            _dbContext.SaveChanges();
            return team;
        }

        public TeamDbo GetTeam(int id)
        {
            var team = _dbContext.Teams
                .Where(o => o.Id == id)
                .SingleOrDefault();
            return team;
        }

        public TeamDbo GetTeamByCode(string code)
        {
            var team = _dbContext.Teams
                .Where(o => o.Code == code)
                .SingleOrDefault();
            return team;
        }

        public List<string> GetTeamPlayers(int teamId)
        {
            return _dbContext.UserTeams
                .Where(o => o.TeamId == teamId && o.UserId != null)
                .Select(o => o.UserId)
                .ToList();
        }
    }
}
