namespace MakeATeamBE.Db.Models
{
    //[PrimaryKey(nameof(UserId), nameof(TeamId))]
    public class UserTeamsDbo
    {
        public string UserId { get; set; }
        public int TeamId { get; set; }
        public string UserNickname { get; set; }
    }
}
