namespace DBGenerator.Models
{
    public class ForeignKeysViewModel
    {
        public List<ForeignKey> ForeignKeys { get; set; } = new List<ForeignKey>();
        public List<string> Tables { get; set; } = new List<string>();
        public List<string> Columnts { get; set; } = new List<string>();
    }
}
