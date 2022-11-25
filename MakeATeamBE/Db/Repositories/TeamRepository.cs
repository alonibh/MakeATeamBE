using MakeATeamBE.Db.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MakeATeamBE.Db.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        public TeamRepository()
        {
            using (var context = new ApiContext())
            {
                if (context.Teams.Count() == 0)
                {
                    var teams = new List<TeamDbo>
                    {
                        new TeamDbo
                        {
                            Id= 1,
                            Name="Team1",
                            AdminId= 1,
                            Code="111",
                            Date=new System.DateTime(2023,1,1),
                            Players = new List<(int Id, string Name)>{(1, "Player1"), (2,"Player2"), (3, "Player3"), (4, "Player4"), (5, "Player5"), (6, "Player6"), (7, "Player7"), (8, "Player8"), (9, "Player9"), (10, "Player10"), (11, "Player11"), (12, "Player12"), (13, "Player13"), (14, "Player14"), (15, "Player15") }
                        },
                    };
                    context.Teams.AddRange(teams);
                    context.SaveChanges();
                }
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
    }
}
