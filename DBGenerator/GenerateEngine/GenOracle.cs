using DBGenerator.Models;


namespace DBGenerator.GenerateEngine
{
    public class GenOracle : IGenData
    {
        public string ctCreateTable(string tableName) => $"CREATE TABLE {tableName}";

        public string ctOpen() => "(";

        public string ctClose() => ")\n";

        public string ctId(string tableName) => $"\tId{tableName} NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,";

       
        public string ctColumn(Column column) => $"\t{column.Name} {GetDataType(column)}";

        public string iInsert(List<Column> columns, Table table)
        {
            var col = String.Join(", ", columns.Select(c => c.Name));
            var inserts = GetValues(table.Datas.ToList(), table.Columns.ToList())
                .Select(v => $"INSERT INTO {table.Name} ({col}) VALUES {v};");

            return string.Join("\n", inserts);
        }

        public string fkGet(string tableName, ForeignKey fk)
        {
            string result = $"ALTER TABLE {tableName}\n";
            result += $"\tADD CONSTRAINT FK_{tableName}_{fk.TablePkName}\n";
            result += $"\tFOREIGN KEY({fk.ColumnFkName}) REFERENCES {fk.TablePkName}(Id{fk.TablePkName});";
            return result;
        }



        private string GetDataType(Column column)
        {
            switch (column.DataType)
            {
                case DataType.Text:
                    return $"VARCHAR2({column.Precision})";
                case DataType.Integer:
                    return "NUMBER";
                case DataType.Decimal:
                    return $"NUMBER({column.Precision})";
                case DataType.Date:
                    return "DATE";
                default:
                    return "VARCHAR2(4000)";
            }
        }

        private string ValueForDatabase(string value, DataType type)
        {
            switch (type)
            {
                case DataType.Text:
                    return $"'{value.Replace("'", "''")}'";                
                case DataType.Date:
                    return $"DATE '{value}'";
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
