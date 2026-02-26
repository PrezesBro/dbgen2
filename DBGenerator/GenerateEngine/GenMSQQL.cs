using DBGenerator.Data;
using DBGenerator.Models;


namespace DBGenerator.GenerateEngine
{
    public class GenMSQQL : IGenData
    {
        private IDataFacade _data;

        public GenMSQQL(IDataFacade data)
        {
            _data = data;
        }


        public string Generate(int databaseId)
        {
            var database = _data.GetDatabaseById(databaseId);
            string x = "";

            x += string.Join("\n", GetScriptCreateTable(database.Tables.ToList()));
            x += "\n\n\n";
            x += string.Join("\n", GetInsertScript(database.Tables.ToList()));
            x += "\n\n\n";
            x += string.Join("\n", GetConstrains(database.Tables.ToList()));
            return x;
        }

        private IEnumerable<string> GetScriptCreateTable(List<Table> tables)
        {
            foreach (var table in tables)
            {
                yield return $"CREATE TABLE {table.Name}";
                yield return "(";
                yield return $"Id{table.Name} INT IDENTITY (1,1) PRIMARY KEY,";

                foreach (var column in table.Columns)
                {
                    yield return $"{column.Name} {GetDataType(column)},";
                }
                yield return ")\n";

            }
        }

        private string GetDataType(Column column)
        {
            switch (column.DataType)
            {
                case DataType.Text:
                    return $"varchar({column.Precision})";
                case DataType.Integer:
                    return "int";
                case DataType.Decimal:
                    return $"decimal({column.Precision})";
                case DataType.Date:
                    return "datetime";
                default:
                    return "varchar(max)";
            }
        }

        private IEnumerable<string> GetInsertScript(List<Table> tables)
        {
            foreach (var table in tables)
            {
                var tabColumns = table.Columns.ToList(); 
                var columns = string.Join(", ", tabColumns.Select(c => c.Name));

                yield return $"\nINSERT INTO {table.Name} ({columns})";
                yield return $"VALUES";               
                yield return string.Join(",\n", GetValues(table.Datas.ToList(), tabColumns));        
            }
        }

        private IEnumerable<string> GetConstrains(List<Table> tables)
        {
            
            foreach (var table in tables)
            {
                if (table.ForeignKeys.Count > 0)
                {

                    foreach (var fk in table.ForeignKeys)
                    {
                        yield return $"ALTER TABLE {table.Name}";
                        yield return $"ADD CONSTRAINT FK_{table.Name}_{fk.TablePkName}";
                        yield return $"FOREIGN KEY({fk.ColumnFkName}) REFERENCES {fk.TablePkName}(Id{fk.TablePkName})";
                        yield return "";
                    }
                }          
            }
        }
     
        private string ValueForDatabase(string value, DataType type)
        {
            switch (type)
            {
                case DataType.Text:
                case DataType.Date:
                    return $"'{value.Replace("'", "''")}'";
                case DataType.Integer:
                    return value; 
                case DataType.Decimal:
                    return value.Replace(',', '.'); 
                default:
                    return $"'{value}'";
            }
        }

        private IEnumerable<string> GetValues(List<Datas> datas, List<Models.Column> tabColumns)
        {
            foreach (var data in datas)
            {
                var values = data.Value.Split(";");
                var vals = new List<string>();
                for (int i = 0; i < tabColumns.Count; i++)
                {
                    var col = tabColumns[i];
                    var dat = values[i];
                    vals.Add(ValueForDatabase(dat, col.DataType));                   
                }
                yield return $"({string.Join(',', vals)})";
            }
        }
    }
}