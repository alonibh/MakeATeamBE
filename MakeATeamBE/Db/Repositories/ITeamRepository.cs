using MakeATeamBE.Db.Models;

namespace MakeATeamBE.Db.Repositories
{
    public interface ITeamRepository
    {
        void AddPlayerToTeam(int teamId, int userId, string name);
        public TeamDbo GetTeam(int id);
        public TeamDbo GetTeamByCode(string code);
    }
}
