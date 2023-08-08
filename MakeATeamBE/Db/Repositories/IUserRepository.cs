using MakeATeamBE.Db.Models;

namespace MakeATeamBE.Db.Repositories
{
    public interface IUserRepository
    {
        public bool AddUser(string userId, string name);
        public void UpdateUser(string userId, string name);
        public UserDbo GetUser(string id);
    }
}
