using MakeATeamBE.Db.Models;
using MakeATeamBE.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakeATeamBE.Db.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        public TeamRepository()
        {
            //using (var context = new ApiContext())
            //{
            //    if (context.Teams.Count() == 0)
            //    {
            //        var teams = new List<TeamDbo>
            //        {
            //            new TeamDbo
            //            {
            //                Name="Team1",
            //                AdminId= 1,
            //                Code="111111",
            //                Date=new System.DateTime(2023,1,1),
            //                Players = new List<(int Id, string Name)>{(1, "Player1"), (2,"Player2"), (3, "Player3"), (4, "Player4"), (5, "Player5"), (6, "Player6"), (7, "Player7"), (8, "Player8"), (9, "Player9"), (10, "Player10"), (11, "Player11"), (12, "Player12"), (13, "Player13"), (14, "Player14"), (15, "Player15") }
            //            },
            //            new TeamDbo
            //            {
            //                Name="Team2",
            //                AdminId= 2,
            //                Code="222222",
            //                Date=new System.DateTime(2023,1,1),
            //                Players = new List<(int Id, string Name)>{(2,"Player2"), (3, "Player3"), (4, "Player4"), (5, "Player5"), (6, "Player6"), (7, "Player7"), (8, "Player8"), (9, "Player9"), (10, "Player10"), (11, "Player11"), (12, "Player12"), (13, "Player13"), (14, "Player14"), (15, "Player15") }
            //            },
            //        };
            //        context.Teams.AddRange(teams);
            //        context.SaveChanges();
            //    }
            //}
        }

        public void AddPlayerToTeam(int teamId, string userId, string name)
        {
            using (var context = new ApiContext())
            {
                var team = context.Teams
                    .Where(o => o.Id == teamId)
                    .SingleOrDefault();
                if (!team.Players.Any(o => o.Id == userId))
                {
                    var updatedPlayersList = new List<(string Id, string Name)>(team.Players)
                    {
                        (userId, name)
                    };
                    team.Players = updatedPlayersList;
                    context.SaveChanges();
                }
            }
        }

        public void AddUserToSubmittedList(int teamId, string userId)
        {
            using (var context = new ApiContext())
            {
                var team = context.Teams
                    .Where(o => o.Id == teamId)
                    .SingleOrDefault();
                if (!team.SubmittedPlayers.Contains(userId))
                {
                    var updatedSubmittedUsersList = new List<string>(team.SubmittedPlayers)
                    {
                        userId
                    };
                    team.SubmittedPlayers = updatedSubmittedUsersList;
                    context.SaveChanges();
                }
            }
        }

        public TeamDbo CreateTeam(string name, string adminId, string adminName, DateTime date)
        {
            using (var context = new ApiContext())
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
                context.Teams.Add(team);
                context.SaveChanges();
                return team;
            }
        }

        public TeamDbo GetTeam(int id)
        {
            using (var context = new ApiContext())
            {
                var team = context.Teams
                    .Where(o => o.Id == id)
                    .SingleOrDefault();
                return team;
            }
        }

        public TeamDbo GetTeamByCode(string code)
        {
            using (var context = new ApiContext())
            {
                var team = context.Teams
                    .Where(o => o.Code == code)
                    .SingleOrDefault();
                return team;
            }
        }
    }
}
