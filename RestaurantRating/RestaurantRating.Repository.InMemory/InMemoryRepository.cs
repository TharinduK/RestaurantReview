using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using RestaurantRating.Domain;

namespace RestaurantRating.Repository.InMemory
{
    public class InMemoryApplicationLog : IApplicationLog
    {
        public void ErrorLog(string v, Exception ex)
        {
            
        }
    }
    public class InMemoryRepository : IRepository
    {
        protected IRepository Repo;
        protected List<Restaurant> Restaurants = new List<Restaurant>();
        protected List<Review> Reviews = new EditableList<Review>();
        protected List<User> Users = new List<User>();
        
        public InMemoryRepository()
        {
            Restaurants.Add(new Restaurant { Name = "Restaurant name one", CreatedBy = 1, UpdatedBy = 1, Cuisine = "Cuisine 1", Id = 1 });
            Restaurants.Add(new Restaurant { Name = "Restaurant name Two", CreatedBy = 1, UpdatedBy = 1, Cuisine = "Cuisine 1", Id = 2 });
            Restaurants.Add(new Restaurant { Name = "Restaurant name Three", CreatedBy = 1, UpdatedBy = 1, Cuisine = "Cuisine 2", Id = 3 });
            Restaurants.Add(new Restaurant { Name = "Restaurant name Four", CreatedBy = 2, UpdatedBy = 2, Cuisine = "Cuisine 2", Id = 4 });
            Restaurants.Add(new Restaurant { Name = "Restaurant name Five", CreatedBy = 2, UpdatedBy = 1, Cuisine = "Cuisine 3", Id = 5 });

            Users.Add(new User { Id = 1 });
            Users.Add(new User { Id = 2 });
            Users.Add(new User { Id = 3 });
            Users.Add(new User { Id = 4 });
            Users.Add(new User { Id = 5 });
            Users.Add(new User { Id = 6 });
            Users.Add(new User { Id = 7 });
            Users.Add(new User { Id = 8 });
            Users.Add(new User { Id = 9 });
            Users.Add(new User { Id = 10 });
            Users.Add(new User { Id = 11 });

            //restaurant 2 setup 
            Reviews.Add(new Review
            {
                CreatedBy = 3,
                ReviewUser = Users[2],
                UpdatedBy = 3,
                //ReviewRestaurant = Restaurants[1],
                Comment = "Comment for 2",
                Rating = 3,
                PostedDateTime = new DateTime(2016, 10, 16),
                ReviewNumber = 1,
            });
            Restaurants[1].AddReview(Reviews[0]);

            //restaurant 3 setup 
            Reviews.Add(new Review
            {
                CreatedBy = 4,
                UpdatedBy = 4,
                Comment = "First Comment for 3",
                Rating = 3,
                PostedDateTime = new DateTime(2016, 10, 16),
                ReviewNumber = 2,
                ReviewUser = Users[3]
            });
            Reviews.Add(new Review
            {
                CreatedBy = 3,
                UpdatedBy = 3,
                Comment = "Second Comment for 3",
                Rating = 5,
                PostedDateTime = new DateTime(2016, 10, 10),
                ReviewNumber = 3,
                ReviewUser = Users[2]
            });
            Restaurants[2].AddReview(Reviews[1]);
            Restaurants[2].AddReview(Reviews[2]);

            //restaurant 4 setup 
            for (var i = 0; i < 10; i++)
            {
                var newReview = new Review
                {
                    CreatedBy = Users[i].Id,
                    UpdatedBy = Users[i].Id,
                    Comment = $"Comment {i} for Restaurant 4",
                    Rating = (i % 5) + 1,
                    PostedDateTime = new DateTime(2016, 09, i + 1),
                    ReviewNumber = 4 + i,
                    ReviewUser = Users[i]
                };
                // 0+1 to 4+1

                Reviews.Add(newReview);

                Restaurants[3].AddReview(Reviews[3 + i]);
            }


        }
        public object GetAdminUser()
        {
            throw new NotImplementedException();
        }

        public int AddRestaurentGetNewId(AddRestaurantRequestModel requestModel)
        {
            Restaurants.Add(new Restaurant
            {
                Id = Restaurants.Count + 1,
                Cuisine = requestModel.Cuisine,
                CreatedBy = requestModel.UserId,
                UpdatedBy = requestModel.UserId,
                Name = requestModel.Name
            });
            return Restaurants.Count;
        }

        public bool DoseRestaurentAlreadyExist(AddRestaurantRequestModel requestModel)
        {
            return Restaurants.Any(r => r.Name.Equals(requestModel.Name.Trim(), StringComparison.CurrentCultureIgnoreCase));
        }

        public void RemoveRestaurentId(RemoveRestaurantRequestModel reqeustModel)
        {
            for (var i = 0; i < Restaurants.Count; i++)
            {
                if (Restaurants[i].Id == reqeustModel.RestaurantId) Restaurants.RemoveAt(i);
            };
        }

        public bool DoseRestaurentIdAlreadyExist(int restaurantId)
        {
            return Restaurants.Any(r => r.Id == restaurantId);
        }

        public Restaurant GetRestaurantById(int restaurantId)
        {
            var foundRestaurant = Restaurants.Find(r => r.Id == restaurantId);
            return foundRestaurant;
        }

        public int AddReviewGetNewId(AddReviewRequestModel reviewToAdd)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public bool DoseUserIdAlreadyExist(int requestUserId)
        {
            throw new NotImplementedException();
        }

        public Restaurant GetRestaurantWithReviewsById(ViewRestaurantRequestModel request)
        {
            return GetRestaurantById(request.RestaurantId);
        }
    }
}
