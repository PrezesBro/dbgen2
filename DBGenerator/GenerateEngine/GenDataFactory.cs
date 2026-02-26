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

        public IGenData Create(EngineType type) => type switch
        {
            EngineType.MSSQL => new GenMSQQL(_data),
            //kolejne silniki
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Nieobsługiwany silnik bazy danych.")
        };
    }
}
