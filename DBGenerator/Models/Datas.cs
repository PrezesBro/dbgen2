namespace DBGenerator.Models
{
    public class Datas
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public virtual Table Table { get; set; }
    }
}
