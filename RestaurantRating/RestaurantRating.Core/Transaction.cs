namespace RestaurantRating.Domain
{
    public abstract class Transaction<T,U> where T:TransactionRequestModel where U:TransactionResponseModel, new()
    {
        protected readonly IApplicationLog ApplicationLog;
        protected readonly IRepository Repository;

        protected Transaction(IRepository repo, IApplicationLog log, T reqeustModel)
        {
            Request = reqeustModel;
            Repository = repo;
            ApplicationLog = log;
        }
        public T Request { get; }
        public U Response { get; set; } = new U();

        public abstract void Execute();
    }
}