using Accessor.Models;

namespace Accessor.Services
{
    public class PhoneBookService
    {

        public async Task<List<PhoneName>> GetAllAsync()
        {
            await Task.Delay(1);

            return new List<PhoneName>()
            {
                new PhoneName()
                {
                    Name = "Test",
                    Phone = "053213211"
                },
                new PhoneName()
                {
                    Name = "Test2",
                    Phone = "0531231234"
                }
            };
        }
    }
}
