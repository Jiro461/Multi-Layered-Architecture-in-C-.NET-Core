using SOA_BaiTap.CoreLayer.Entities;

namespace SOA_BaiTap.BusinessLayer
{
    public class RatingCalculator
    {
        public static decimal CalculateAverageRating(IEnumerable<Rating> ratings)
        {
            if (ratings == null || !ratings.Any())
                return 0;
            return ratings.Average(r => r.Value);
        }
    }
}
