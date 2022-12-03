using Microsoft.EntityFrameworkCore;

namespace MakeATeamBE.Db.Models
{
    [PrimaryKey(nameof(RatingGiverId), nameof(RatingSubjectId))]
    public class RatingDbo
    {
        public int RatingGiverId { get; set; }
        public int RatingSubjectId { get; set; }
        public int Rating { get; set; }
    }
}
