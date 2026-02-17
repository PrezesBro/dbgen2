using DBGenerator.Models;

namespace DBGenerator.GenerateEngine
{
    public interface IGenDataFactory
    {
        IGenData Create(EngineType type);
    }
}
