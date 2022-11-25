using MakeATeamBE.Db.Models;

namespace MakeATeamBE.Db.Repositories
{
    public interface ITeamRepository
    {
        public TeamDbo GetTeam(int id);
    }
}
