using System;

namespace RestaurantRating.Domain
{
    public interface IApplicationLog
    {
        void ErrorLog(string v, Exception ex);
        void InformationLog(string v);
    }
}