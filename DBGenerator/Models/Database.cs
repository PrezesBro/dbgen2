namespace DBGenerator.Models
{
    public class Database
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public DateTime CreateDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Table> Tables { get; set; } = new List<Table>();
    }
}
