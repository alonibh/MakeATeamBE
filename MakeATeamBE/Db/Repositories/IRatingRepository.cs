using MakeATeamBE.Db.Models;
using MakeATeamBE.Models;
using System.Collections.Generic;

namespace MakeATeamBE.Db.Repositories
{
    public interface IRatingRepository
    {
        public RatingDbo GetRating(int ratingGiverId, int ratingSubjectId);
        public List<RatingDbo> GetUserRatings(int ratingGiverId);
        public void SetUserRatings(int userId, List<UserRating> ratings);
    }
}
