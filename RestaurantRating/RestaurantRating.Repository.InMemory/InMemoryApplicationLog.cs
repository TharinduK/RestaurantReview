using System;
using RestaurantRating.Domain;
using System.Diagnostics;

namespace RestaurantRating.Repository.InMemory
{
    public class InMemoryApplicationLog : IApplicationLog
    {
        public void ErrorLog(string message, Exception ex)
        {
            Debug.WriteLine($"Error:{message}");
            Debug.WriteLine($"Exception: {ex.Message}");
        }

        public void InformationLog(string message)
        {
            Debug.WriteLine($"Information:{message}");
        }
    }
}