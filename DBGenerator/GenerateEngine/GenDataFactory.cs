using DBGenerator.Data;
using DBGenerator.Models;

namespace DBGenerator.GenerateEngine
{
    public class GenDataFactory : IGenDataFactory
    {
        private IDataFacade _data;

        public GenDataFactory(IDataFacade data)
        {
            _data = data;
        }

        public GenEngine Create(EngineType type) => type switch
        {
            EngineType.MSSQL => new GenEngine(new GenMSQQL(), _data),
            
            EngineType.Oracle => new GenEngine(new GenOracle(), _data), 

            EngineType.MySQL => new GenEngine(new GenMySQL(), _data), 

            _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Nieobsługiwany silnik bazy danych.")
        };
    }
}
