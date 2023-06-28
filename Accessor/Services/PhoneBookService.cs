using Accessor.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Accessor.Services
{
    public class PhoneBookService
    {
        private readonly DbContext _db;
        public PhoneBookService(DbContext db)
        {
            _db = db;
        }

        public async Task<List<PhoneName>?> GetAllAsync()
        {
            try
            {
                var result = await _db.phoneNameCollection.Find(new BsonDocument()).ToListAsync();

                return result.Select(FromDto).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PhoneName>?> GetByNameAsync(string name)
        {
            try
            {
                var result = await _db.phoneNameCollection.Find(row => row.Name == name).ToListAsync();

                return result.Select(FromDto).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PhoneName?> AddPhoneNameAsync(PhoneName phoneName)
        {
            try
            {
                await _db.phoneNameCollection.InsertOneAsync(ToDto(phoneName));

                return phoneName;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private PhoneName FromDto(PhoneNameDto phoneName)
        {
            return new PhoneName()
            {
                Name = phoneName.Name,
                Phone = phoneName.Phone
            };
        }

        private PhoneNameDto ToDto(PhoneName phoneName)
        {
            return new PhoneNameDto()
            {
                Name = phoneName.Name,
                Phone = phoneName.Phone
            };
        }
    }
}
