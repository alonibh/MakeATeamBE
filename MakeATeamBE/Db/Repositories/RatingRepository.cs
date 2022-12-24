using MakeATeamBE.Db.Models;
using MakeATeamBE.Models;
using System.Collections.Generic;
using System.Linq;

namespace MakeATeamBE.Db.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        public RatingRepository()
        {
            //using (var context = new ApiContext())
            //{
            //    if (context.Ratings.Count() == 0)
            //    {
            //        var ratings = new List<RatingDbo>
            //        {
            //            new RatingDbo
            //            {
            //                RatingGiverId= 1,
            //                RatingSubjectId=2,
            //                Rating=2
            //            },
            //             new RatingDbo
            //            {
            //                RatingGiverId= 1,
            //                RatingSubjectId=3,
            //                Rating=1
            //            },
            //             new RatingDbo
            //            {
            //                RatingGiverId= 2,
            //                RatingSubjectId=1,
            //                Rating=4
            //            },
            //             new RatingDbo
            //            {
            //                RatingGiverId= 2,
            //                RatingSubjectId=3,
            //                Rating=5
            //            },
            //             new RatingDbo
            //            {
            //                RatingGiverId= 3,
            //                RatingSubjectId=1,
            //                Rating=1
            //            },
            //             new RatingDbo
            //            {
            //                RatingGiverId= 3,
            //                RatingSubjectId=2,
            //                Rating=4
            //            }
            //        };
            //        context.Ratings.AddRange(ratings);
            //        context.SaveChanges();
            //    }
            //}
        }

        public RatingDbo GetRating(string ratingGiverId, string ratingSubjectId)
        {
            using (var context = new ApiContext())
            {
                var rating = context.Ratings
                    .Where(o => o.RatingGiverId == ratingGiverId && o.RatingSubjectId == ratingSubjectId)
                    .SingleOrDefault();
                return rating;
            }
        }

        public List<RatingDbo> GetUserRatings(string ratingGiverId)
        {
            using (var context = new ApiContext())
            {
                var ratings = context.Ratings
                    .Where(o => o.RatingGiverId == ratingGiverId)
                    .ToList();
                return ratings;
            }
        }

        public void SetUserRatings(string userId, List<UserRating> ratings)
        {
            using (var context = new ApiContext())
            {
                var userRatings = context.Ratings.Where(o => o.RatingGiverId == userId);

                foreach (var rating in ratings)
                {
                    if (!userRatings.Any(o => o.RatingSubjectId == rating.UserId))
                        context.Ratings.Add(new RatingDbo
                        {
                            RatingGiverId = userId,
                            RatingSubjectId = rating.UserId,
                            Rating = rating.Rating
                        });

                    else
                        context.Ratings.Where(o => o.RatingGiverId == userId && o.RatingSubjectId == rating.UserId).Single().Rating = rating.Rating;
                }
                context.SaveChanges();
            }
        }
    }
}
