namespace DBGenerator.Models
{
    public class ForeignKey
    {
        public int Id { get; set; }
        public string ColumnFkName { get; set; }
        public string TablePkName { get; set; }
        public Table Table { get; set; }
    }
}
