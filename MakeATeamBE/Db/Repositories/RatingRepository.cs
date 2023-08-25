using MakeATeamBE.Db.Models;
using MakeATeamBE.Models;
using System.Collections.Generic;
using System.Linq;

namespace MakeATeamBE.Db.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly MakeATeamContext _dbContext;

        public RatingRepository(MakeATeamContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<RatingDbo> GetUserRatings(string ratingGiverId)
        {
            var ratings = _dbContext.Ratings
                .Where(o => o.RatingGiverId == ratingGiverId)
                .ToList();
            return ratings;
        }

        public void SetUserRatings(string userId, List<UserRating> ratings)
        {
            var userRatings = _dbContext.Ratings.Where(o => o.RatingGiverId == userId);

            foreach (var rating in ratings)
            {

                if (!userRatings.Any(o => o.RatingSubjectNickname == rating.SubjectNickname))
                    _dbContext.Ratings.Add(new RatingDbo
                    {
                        RatingGiverId = userId,
                        RatingSubjectNickname = rating.SubjectNickname,
                        Rating = rating.Rating
                    });

                else
                    _dbContext.Ratings.Where(o => o.RatingGiverId == userId && o.RatingSubjectNickname == rating.SubjectNickname).Single().Rating = rating.Rating;
            }
            _dbContext.SaveChanges();
        }
    }
}
