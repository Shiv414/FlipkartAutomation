using Newtonsoft.Json;
using System.IO;

namespace FlipkartTests.Utilities
{
    public static class TestDataReader
    {
        public static dynamic GetTestData()
        {
            string filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "TestData.json");
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject(json);
        }
    }
}
