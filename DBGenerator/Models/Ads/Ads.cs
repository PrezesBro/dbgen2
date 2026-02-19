using System.ComponentModel.DataAnnotations;

namespace DBGenerator.Models.Ads
{
    public class Ads
    {
        public int Id { get; set; }
        [Display(Name = "Umiejscowienie")]
        public Position Position { get; set; }
        [Display(Name = "Czy widoczna")]
        public bool IsVisible { get; set; }
        [Display(Name = "Kolor tła")]
        public string BackgroundColor { get; set; }
        [Display(Name = "Kolejność")]
        public int Order { get; set; }

        [Display(Name = "Url grafiki")]
        public string ImageUrl { get; set; }
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        [Display(Name = "Treść reklamy")]
        public string Description { get; set; }
        [Display(Name = "Url docelowe")]
        public string DestinationUrl { get; set; }
        [Display(Name = "Cena")]
        public decimal Price { get; set; }

        [Display(Name = "Czy promocja")]
        public bool IsPromotion { get; set; }
        [Display(Name = "Data zakończenia promocji")]
        public DateTime EndPromotion { get; set; }
        [Display(Name = "Cena promocyjna")]
        public decimal PromoPrice { get; set; }
    }
}
