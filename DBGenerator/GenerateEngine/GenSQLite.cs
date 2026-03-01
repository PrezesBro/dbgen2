using DBGenerator.Models;

namespace DBGenerator.GenerateEngine
{
    public class GenSQLite : IGenData
    {
        public string ctCreateTable(string tableName) => $"CREATE TABLE {tableName}";

        public string ctOpen() => "(";

        public string ctClose() => ");\n";

        public string ctId(string tableName) => $"\tId{tableName} INTEGER PRIMARY KEY AUTOINCREMENT,";


        public string ctColumn(Column column) => $"\t{column.Name} {GetDataType(column)}";

        public string iInsert(List<Column> columns, Table table)
        {
            var col = String.Join(", ", columns.Select(c => c.Name));
            var result = $"INSERT INTO {table.Name} ({col})\n";
            result += "VALUES ";
            result += string.Join(",\n\t", GetValues(table.Datas.ToList(), table.Columns.ToList()));
            result += ";";
            return result;
        }

        public string fkGet(string tableName, ForeignKey fk)
        {
            return $"\tFOREIGN KEY({fk.ColumnFkName}) REFERENCES {fk.TablePkName}(Id{fk.TablePkName})";                  
        }



        private string GetDataType(Column column)
        {
            switch (column.DataType)
            {
                case DataType.Text:
                    return $"TEXT";
                case DataType.Integer:
                    return "INTEGER";
                case DataType.Decimal:
                    return $"REAL";
                case DataType.Date:
                    return "TEXT";
                default:
                    return "TEXT";
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

        public string ctColumnsDefinition(IEnumerable<Column> columns)
        {
            return string.Join("\n", columns.Select(c => ctColumn(c) + ","));
        }

        public IEnumerable<string> GetForeignKeysWithComma(Table table)
        {
            var fks = table.ForeignKeys;
            if (fks.Count == 0) yield break;

            int index = 0;
            foreach (var fk in fks)
            {
                var fkLine = fkGet(table.Name, fk);

                if (fks.Count > 1 && index < fks.Count - 1)
                    fkLine += ",";

                yield return fkLine;
                index++;
            }
        }
        //metoda która wstawia kolumny i klucze obce tak żeby przecinki się zgadzały 
        public IEnumerable<string> ctColumnsAndForeignKeys(Table table)
        {
            var colList = table.Columns.ToList();
            var fkList = table.ForeignKeys.ToList();

            int totalRows = colList.Count + fkList.Count; 
            int index = 0;

            foreach (var c in colList)
            {
                bool addComma = (index < totalRows - 1);
                yield return ctColumn(c) + (addComma ? "," : "");
                index++;
            }

            foreach (var fk in fkList)
            {
                bool addComma = (index < totalRows - 1);
                yield return fkGet(table.Name, fk) + (addComma ? "," : "");
                index++;
            }
        }
    }
}
