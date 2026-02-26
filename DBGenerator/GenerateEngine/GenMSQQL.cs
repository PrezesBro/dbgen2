using DBGenerator.Data;

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
            return "--komentarz\nSELECT 'Jakiś skrypcik SQL'";
        }
    }
}
