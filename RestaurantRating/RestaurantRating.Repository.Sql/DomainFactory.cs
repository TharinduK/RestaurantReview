namespace RestaurantRating.Repository.Sql
{
    public class DomainFactory
    {
        public static Domain.Cuisine CreateCuisine(Sql.Cuisine cuisineIn)
        {
            if (cuisineIn != null)
                return (new Domain.Cuisine
                {
                    Id = cuisineIn.Id,
                    CreatedBy = cuisineIn.CreatedBy,
                    Name = cuisineIn.Name,
                    UpdatedBy = cuisineIn.UpdatedBy
                });
            else
                return null;
        }

        public static Domain.User CreateUser(Sql.AppUser appUser)
        {
            if (appUser != null)
                return new Domain.User
                {
                    Id = appUser.Id,
                    UserName = appUser.UserName,
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                };
            else
                return null;
        }

        public static Domain.Restaurant CreateRestaurant(Sql.Restaurant restaurnatIn)
        {
            if (restaurnatIn != null)
                return new Domain.Restaurant
                {
                    Id = restaurnatIn.Id,
                    Cuisine = new Domain.Cuisine { Id = restaurnatIn.CuisineId },
                    Name = restaurnatIn.Name,
                    CreatedBy = restaurnatIn.CreatedBy,
                    UpdatedBy = restaurnatIn.UpdatedBy
                };
            else
                return null;

        }
        public static Domain.Restaurant CreateRestaurantWithReivew(Sql.Restaurant restaurnatIn)
        {

            if (restaurnatIn != null)
            {
                var resttaurantOut = CreateRestaurant(restaurnatIn);

                foreach (var reviewIn in restaurnatIn.Reviews) resttaurantOut.AddReview(CreateReview(reviewIn));

                return resttaurantOut;
            }
            else
                return null;

        }

        public static Domain.Review CreateReview(Sql.Review reviewIn)
        {
            if (reviewIn != null)
            {
                return new Domain.Review
                {
                    ReviewNumber = reviewIn.ReviewNumber,
                    Comment = reviewIn.Comment,
                    CreatedBy = reviewIn.CreatedBy,
                    PostedDateTime = reviewIn.PostedDate,
                    Rating = reviewIn.Rating,
                    ReviewUser =  CreateUser(reviewIn.AppUser),
                    UpdatedBy = reviewIn.UpdatedBy
                };
            }
            else
                return null;
        }
    }
}
