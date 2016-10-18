namespace RestaurantRating.Domain
{
    public class AddAdminTransaction :Transaction<AddAdminRequestModel, AddAdminResponseModel>
    {
        public AddAdminTransaction(IRepository repo, IApplicationLog log, AddAdminRequestModel reqeustModel) : base(repo, log, reqeustModel)
        {
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }

    }
}