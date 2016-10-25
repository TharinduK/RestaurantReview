namespace RestaurantRating.Domain
{
    public class AuditRecord
    {
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}