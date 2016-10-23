using System;
using System.Runtime.Serialization;

namespace RestaurantRating.Domain.UseCases
{
    [Serializable]
    class CuisineNotFoundException : Exception
    {
        public CuisineNotFoundException()
        {
        }

        public CuisineNotFoundException(string message) : base(message)
        {
        }

        public CuisineNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CuisineNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}