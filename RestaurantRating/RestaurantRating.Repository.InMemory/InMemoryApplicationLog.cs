using System;
using RestaurantRating.Domain;

namespace RestaurantRating.Repository.InMemory
{
    public class InMemoryApplicationLog : IApplicationLog
    {
        public void ErrorLog(string v, Exception ex)
        {
            
        }

        public void InformationLog(string v)
        {
            
        }
    }
}