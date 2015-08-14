namespace RealEstate.App_Start
{
    using MongoDB.Driver;
    using RealEstate.Properties;
    using RealEstate.Rentals;

    public class RealEstateContext
    {
        private readonly IMongoDatabase database;

        public RealEstateContext()
        {
            var client = new MongoClient(Settings.Default.RealEstateConnectionString);
            database = client.GetDatabase(Settings.Default.RealEstateDatabaseName);
        }

        public IMongoDatabase Database
        {
            get { return database; }
        }

        public IMongoCollection<Rental> Rentals
        {
            get { return Database.GetCollection<Rental>("rentals"); }
        }
    }
}