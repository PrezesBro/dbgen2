using System.ComponentModel.DataAnnotations;

namespace DBGenerator.Models
{
    public class Database
    {
        public int Id { get; set; }
        [Display(Name="Wersja")]
        public int Version { get; set; }
        [Display(Name = "Data utworzenia")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Display(Name = "Widoczne")]
        public bool IsVisible { get; set; }
        public virtual ICollection<Table> Tables { get; set; } = new List<Table>();
    }
}
