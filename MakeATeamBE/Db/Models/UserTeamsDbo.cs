using Microsoft.EntityFrameworkCore;

namespace MakeATeamBE.Db.Models
{
    [PrimaryKey(nameof(TeamId), nameof(UserNickname))]
    public class UserTeamsDbo
    {
        public int TeamId { get; set; }
        public string UserNickname { get; set; }
        public string UserId { get; set; }
    }
}
