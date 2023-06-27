using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Accessor.Models
{
    public class DbContext
    {
        public readonly IMongoCollection<PhoneNameDto> phoneNameCollection;

        public DbContext(IOptions<PhoneBookDbSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);

            phoneNameCollection = client
                .GetDatabase(options.Value.DatabaseName)
                .GetCollection<PhoneNameDto>(options.Value.PhoneBookCollectionName);
        }
    }
}
