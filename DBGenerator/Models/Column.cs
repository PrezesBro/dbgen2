using Microsoft.Identity.Client;
using static System.Net.Mime.MediaTypeNames;

namespace DBGenerator.Models
{
    public class Column
    {       
        public int Id { get; set; }
        public string Name { get; set; }
        public DataType DataType { get; set; }
        public string Precision { get; set; }
        public virtual Table Table { get; set; }

        public List<KeyValuePair<string, string>> DataTypes => new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("0", "Tekst"),
            new KeyValuePair<string, string>("1", "L. całkowita"),
            new KeyValuePair<string, string>("2", "L. dziesiętna"),
            new KeyValuePair<string, string>("3", "Data i czas"),
        };
    }
}
