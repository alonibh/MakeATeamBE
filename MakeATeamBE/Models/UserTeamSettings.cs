using System.Collections.Generic;

namespace MakeATeamBE.Models
{
    public class UserTeamSettings
    {
        public IEnumerable<UserRating> Ratings { get; set; }
        public string Name { get; set; }
        public bool IsUserAdminOfTeam { get; set; }
        public int UnsubmittedPlayersCount { get; set; }
    }
}
