using DBGenerator.Models;

namespace DBGenerator.GenerateEngine
{
    public interface IGenDataFactory
    {
        GenEngine Create(EngineType type);
    }
}
