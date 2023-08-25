using MakeATeamBE.Db.Models;
using MakeATeamBE.Models;
using System.Collections.Generic;

namespace MakeATeamBE.Db.Repositories
{
    public interface IRatingRepository
    {
        public List<RatingDbo> GetUserRatings(string ratingGiverId);
        public void SetUserRatings(string userId, List<UserRating> ratings);
    }
}
