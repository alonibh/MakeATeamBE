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

        public void AddPlayerToTeam(int teamId, string userId, string name)
        {
            var team = _dbContext.Teams
                .Where(o => o.Id == teamId)
                .SingleOrDefault();
            if (!team.Players.Any(o => o.Id == userId))
            {
                var updatedPlayersList = new List<(string Id, string Name)>(team.Players)
                    {
                        (userId, name)
                    };
                team.Players = updatedPlayersList;
                _dbContext.SaveChanges();
            }
        }

        public void AddUserToSubmittedList(int teamId, string userId)
        {
            var team = _dbContext.Teams
                .Where(o => o.Id == teamId)
                .SingleOrDefault();
            if (!team.SubmittedPlayers.Contains(userId))
            {
                var updatedSubmittedUsersList = new List<string>(team.SubmittedPlayers)
                    {
                        userId
                    };
                team.SubmittedPlayers = updatedSubmittedUsersList;
                _dbContext.SaveChanges();
            }
        }

        public TeamDbo CreateTeam(string name, string adminId, string adminName, DateTime date)
        {
            var team = new TeamDbo
            {
                Name = name,
                AdminId = adminId,
                Code = CommonUtils.CreateRandomTeamCode(),
                Date = date,
                Players = new List<(string Id, string Name)> { (adminId, adminName) },
                SubmittedPlayers = new List<string>()
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
    }
}
