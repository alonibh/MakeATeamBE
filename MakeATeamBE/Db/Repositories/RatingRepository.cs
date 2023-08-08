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

        public RatingDbo GetRating(string ratingGiverId, string ratingSubjectId)
        {
            var rating = _dbContext.Ratings
                .Where(o => o.RatingGiverId == ratingGiverId && o.RatingSubjectId == ratingSubjectId)
                .SingleOrDefault();
            return rating;
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
                if (!userRatings.Any(o => o.RatingSubjectId == rating.UserId))
                    _dbContext.Ratings.Add(new RatingDbo
                    {
                        RatingGiverId = userId,
                        RatingSubjectId = rating.UserId,
                        Rating = rating.Rating
                    });

                else
                    _dbContext.Ratings.Where(o => o.RatingGiverId == userId && o.RatingSubjectId == rating.UserId).Single().Rating = rating.Rating;
            }
            _dbContext.SaveChanges();
        }
    }
}
