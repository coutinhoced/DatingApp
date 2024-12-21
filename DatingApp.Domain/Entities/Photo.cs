using DatingApp.Domain.Common;

namespace DatingApp.Domain.Entities
{
    public class Photo : EntityBase
    {
        public int Id { get; set; } 

        public string Url { get; set; }   

        public bool IsMain { get; set; }
        public string? PublicId { get; set; }

        public int UserId { get; set; }

    }
}