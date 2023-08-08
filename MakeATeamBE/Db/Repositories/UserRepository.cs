using MakeATeamBE.Db.Models;
using System.Collections.Generic;
using System.Linq;

namespace MakeATeamBE.Db.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MakeATeamContext _dbContext;

        public UserRepository(MakeATeamContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <returns>True if new user added, False otherwise</returns>

        public bool AddUser(string userId, string name)
        {
            if (_dbContext.Users.SingleOrDefault(o => o.Id == userId) == null)
            {
                var user = new UserDbo
                {
                    Id = userId,
                    Name = name,
                    Teams = new List<int> { },
                };
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public void UpdateUser(string userId, string name)
        {
            var user = _dbContext.Users.Single(o => o.Id == userId);
            user.Name = name;
            _dbContext.SaveChanges();
        }

        public UserDbo GetUser(string id)
        {
            var user = _dbContext.Users
                .Where(o => o.Id == id)
                .SingleOrDefault();
            return user;
        }
    }
}
