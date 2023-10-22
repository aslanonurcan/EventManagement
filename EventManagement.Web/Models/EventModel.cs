using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Web.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        [DisplayName("Başlık")]
        public string Title { get; set; }
        [DisplayName("Yer")]
        public string Location { get; set; }
        [DisplayName("Zaman")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [DataType(DataType.Date)]
        public DateTime Time { get; set; }
        [DisplayName("Ücretli")]
        public bool IsPaid { get; set; }
        [DisplayName("Ücret")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Price { get; set; }
        [DisplayName("Açıklama")]
        public string Description { get; set; }
        [DisplayName("Görsel")]
        public string Image { get; set; }
        [DisplayName("Görsel")]
        public IFormFile? ImageFile { get; set; }
        [DisplayName("Ücret")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public string PriceString { get; set; }
    }
}
