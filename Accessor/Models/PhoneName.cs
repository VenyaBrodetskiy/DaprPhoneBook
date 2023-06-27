using MongoDB.Bson;

namespace Accessor.Models
{
    public record PhoneName
    {
        public required string Name { get; set; }
        public required string Phone { get; set; }
    }

    public record PhoneNameDto
    {
        public ObjectId Id { get; set; }
        public required string Name { get; set; }
        public required string Phone { get; set; }
    }
}
