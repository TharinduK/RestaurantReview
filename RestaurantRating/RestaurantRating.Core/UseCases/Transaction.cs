namespace RestaurantRating.Domain
{
    public abstract class Transaction<T,TU> where T:TransactionRequestModel where TU:TransactionResponseModel, new()
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
        public TU Response { get; set; } = new TU();

        public abstract void Execute();
    }
}