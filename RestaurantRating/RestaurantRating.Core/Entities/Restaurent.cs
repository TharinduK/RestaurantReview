namespace RestaurantRating.Domain
{
    public class Restaurant : AuditRecord
    {
        private string _cuisine;
        private string _name;

        public string Cuisine
        {
            get { return _cuisine; }
            set { _cuisine = value.Trim(); }
        }
        public int Id { get; set; }
        public string Name
        {
            get { return _name; }
            set { _name = value.Trim(); }
        }
    }
}