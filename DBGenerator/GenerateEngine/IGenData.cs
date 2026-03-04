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
        public string ctColumns(Table table);
        
        string iInsert(List<Column> columns, Table table);

        string fkGet(string tableName, ForeignKey fk);
        IEnumerable<string> GetForeignKeys(List<Table> tables);
    }
}
//dodać opcej usunięcia bd
//edytuj -> edytuj dokończyć 3 przyciski 
//edytuj kolumnty -> zmiena typu danych z rozwijanej listy i zmiana nazwy i prezycji 
//edytuj dane ->  lista jak w notatniku w momencie zapisu usuwa dane i dodaje nowe 
//klucze obce tabele i klucze: dwie listy rozwijane, lista kolumn tabeli którą edytuję, lista tabel 