using MakeATeamBE.Db.Models;
using MakeATeamBE.Utils;
using System.Collections.Generic;
using System.Linq;

namespace MakeATeamBE.Db.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository()
        {
            #region Template
            //using (var context = new ApiContext())
            //{
            //    if (context.Users.Count() == 0)
            //    {
            //        var users = new List<UserDbo>
            //        {
            //            new UserDbo
            //            {
            //                Id= 1,
            //                Name="Player1",
            //                Teams=new List<int>{ 1 },
            //            },
            //            new UserDbo
            //            {
            //               Id=2,
            //               Name="Player2",
            //               Teams=new List<int>{ 1,2 }
            //            },
            //            new UserDbo
            //            {
            //               Id=3,
            //               Name="Player3",
            //               Teams=new List<int>{ 1,2 }
            //            },
            //            new UserDbo
            //            {
            //                Id= 4,
            //                Name="Player4",
            //                Teams=new List<int>{ 1, 2 },
            //            },
            //            new UserDbo
            //            {
            //               Id=5,
            //               Name="Player5",
            //               Teams=new List<int>{ 1, 2 }
            //            },
            //            new UserDbo
            //            {
            //               Id=6,
            //               Name="Player6",
            //               Teams=new List<int>{ 1, 2 }
            //            },
            //            new UserDbo
            //            {
            //                Id= 7,
            //                Name="Player7",
            //                Teams=new List<int>{ 1, 2 },
            //            },
            //            new UserDbo
            //            {
            //               Id=8,
            //               Name="Player8",
            //               Teams=new List<int>{ 1, 2 }
            //            },
            //            new UserDbo
            //            {
            //               Id=9,
            //               Name="Player9",
            //               Teams=new List<int>{ 1, 2 }
            //            },
            //            new UserDbo
            //            {
            //                Id= 10,
            //                Name="Player10",
            //                Teams=new List<int>{ 1, 2 },
            //            },
            //            new UserDbo
            //            {
            //               Id=11,
            //               Name="Player11",
            //               Teams=new List<int>{ 1, 2 }
            //            },
            //            new UserDbo
            //            {
            //               Id=12,
            //               Name="Player12",
            //               Teams=new List<int>{ 1, 2 }
            //            },
            //            new UserDbo
            //            {
            //                Id= 13,
            //                Name="Player13",
            //                Teams=new List<int>{ 1, 2 },
            //            },
            //            new UserDbo
            //            {
            //               Id=14,
            //               Name="Player14",
            //               Teams=new List<int>{ 1, 2 }
            //            },
            //            new UserDbo
            //            {
            //               Id=15,
            //               Name="Player15",
            //               Teams=new List<int>{ 1, 2 }
            //            }
            //        };
            //        context.Users.AddRange(users);
            //        context.SaveChanges();
            //    }
            //}
            #endregion
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <returns>True if new user added, False otherwise</returns>

        public bool AddUser(string userId, string name)
        {
            using (var context = new ApiContext())
            {
                if (context.Users.SingleOrDefault(o => o.Id == userId) == null)
                {

                    var user = new UserDbo
                    {
                        Id = userId,
                        Name = name,
                        Teams = new List<int> { },
                    };
                    context.Users.Add(user);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }



        public void AddUserToTeam(string userId, int teamId)
        {
            using (var context = new ApiContext())
            {
                var user = context.Users
                    .Where(o => o.Id == userId)
                    .SingleOrDefault();

                if (!user.Teams.Any(o => o == teamId))
                {
                    var updatedTeamsList = new List<int>(user.Teams)
                    {
                        teamId
                    };
                    user.Teams = updatedTeamsList;
                    context.SaveChanges();
                }
            }
        }

        public UserDbo GetUser(string id)
        {
            using (var context = new ApiContext())
            {
                var user = context.Users
                    .Where(o => o.Id == id)
                    .SingleOrDefault();
                return user;
            }
        }
    }
}
