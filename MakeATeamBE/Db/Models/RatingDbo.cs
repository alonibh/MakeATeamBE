using Microsoft.EntityFrameworkCore;

namespace MakeATeamBE.Db.Models
{
    [PrimaryKey(nameof(RatingGiverId), nameof(RatingSubjectId))]
    public class RatingDbo
    {
        public string RatingGiverId { get; set; }
        public string RatingSubjectId { get; set; }
        public int Rating { get; set; }
    }
}
