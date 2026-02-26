using DBGenerator.Data;
using DBGenerator.Models;

namespace DBGenerator.GenerateEngine
{
    public class GenEngine
    {
        private readonly IGenData _gen;
        private readonly IDataFacade _data;
        public GenEngine(IGenData gen, IDataFacade data) 
        {
            _gen = gen;
            _data = data;
        }

        public string Generate(int databaseId)
        {
            string result = string.Empty;

            var database = _data.GetDatabaseById(databaseId);
            var tables = database.Tables.ToList();

            result += String.Join("\n", GetScriptCreateTable(tables));
            result += String.Join("\n", GetInsertScript(tables));
            result += String.Join("\n", GetForeignKeys(tables));

            return result;
        }

        public async Task<string> GenerateAsync(int databaseId)
        {
            var database = _data.GetDatabaseById(databaseId);
            var tables = database.Tables.ToList();

            var ctTask = Task.Run(() => GetScriptCreateTable(tables));
            var iTask = Task.Run(() => GetInsertScript(tables));
            var fkTask = Task.Run(() => GetForeignKeys(tables));

            var results = await Task.WhenAll(ctTask, iTask, fkTask);

            return String.Join("\n", results[0]) +
                String.Join("\n", results[1]) +
                String.Join("\n", results[2]);
        }

        private IEnumerable<string> GetScriptCreateTable(List<Table> tables)
        {
            foreach (var table in tables)
            {
                yield return _gen.ctCreateTable(table.Name);
                yield return _gen.ctOpen();
                yield return _gen.ctId(table.Name);

                foreach(var col in table.Columns)
                {
                    yield return _gen.ctColumn(col);
                }
                yield return _gen.ctClose();
            }
            yield return "\n\n";
        }

        private IEnumerable<string> GetInsertScript(List<Table> tables)
        {
            foreach(var table in tables)
            {
                var columns = table.Columns.ToList();

                yield return _gen.iInsert(columns, table);
                yield return String.Empty;
            }
            yield return "\n\n";
        }

        private IEnumerable<string> GetForeignKeys(List<Table> tables)
        {
            foreach(var table in tables)
            {
                if(table.ForeignKeys.Count > 0)
                {
                    foreach(var fk in table.ForeignKeys)
                    {
                        yield return _gen.fkGet(table.Name, fk);
                        yield return String.Empty;
                    }
                }
            }
        }
    }
}
