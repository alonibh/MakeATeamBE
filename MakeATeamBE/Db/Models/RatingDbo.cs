using Microsoft.EntityFrameworkCore;

namespace MakeATeamBE.Db.Models
{
    [PrimaryKey(nameof(RatingGiverId), nameof(RatingSubjectNickname))]
    public class RatingDbo
    {
        public string RatingGiverId { get; set; }
        public string RatingSubjectNickname { get; set; }
        public int Rating { get; set; }
    }
}
