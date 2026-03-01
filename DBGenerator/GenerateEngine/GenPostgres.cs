using DBGenerator.Models;

namespace DBGenerator.GenerateEngine
{
    public class GenPostgres : IGenData
    {
        public string ctCreateTable(string tableName) => $"CREATE TABLE {tableName}";

        public string ctOpen() => "(";

        public string ctClose() => ");\n";

        public string ctId(string tableName) => $"\tId{tableName} INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,";


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
            string result = $"ALTER TABLE {tableName}\n";
            result += $"ADD CONSTRAINT FK_{tableName}_{fk.TablePkName}\n";
            result += $"FOREIGN KEY({fk.ColumnFkName})\nREFERENCES {fk.TablePkName}(Id{fk.TablePkName});";
            return result;
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
                    return "timestamp";
                default:
                    return "varchar(max)";
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
            return string.Join(",\n", columns.Select(c => ctColumn(c)));
        }
    }
}
