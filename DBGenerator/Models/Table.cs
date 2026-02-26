namespace DBGenerator.Models
{
    public class Table
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Database Database { get; set; }
        public virtual ICollection<Column> Columns { get; set; }
        public virtual ICollection<Datas> Datas { get; set; }
        public virtual ICollection<ForeignKey> ForeignKeys { get; set; }
    }
}
