using Microsoft.Identity.Client;

namespace DBGenerator.Models
{
    public class Column
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DataType DataType { get; set; }
        public string Precision { get; set; }
        public virtual Table Table { get; set; }
    }
}
