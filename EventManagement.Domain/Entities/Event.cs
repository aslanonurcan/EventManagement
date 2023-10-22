using EventManagement.Domain.Common;

namespace EventManagement.Domain.Entities
{
    public class Event : BaseEntity
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime Time { get; set; }
        public bool IsPaid { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
       
    }
}
