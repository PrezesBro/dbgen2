using DBGenerator.Models;

namespace DBGenerator.GenerateEngine
{
    public interface IGenData
    {
        string ctCreateTable(string tableName);
        string ctOpen();
        string ctClose();
        string ctId(string tableName);
        string ctColumn(Column column);
        public string ctColumnsDefinition(IEnumerable<Column> columns);
        
        string iInsert(List<Column> columns, Table table);

        string fkGet(string tableName, ForeignKey fk);
    }
}
