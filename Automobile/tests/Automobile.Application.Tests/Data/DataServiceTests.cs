namespace Automobile.Application.Tests.Data
{
    public class DataServiceTests
    {
        private readonly Application.Data.DataService _dataService;

        public DataServiceTests()
        {
            _dataService = new Application.Data.DataService();
        }

        [Fact]
        [UnitTest]
        public void GetData_ReturnExpected_WhenNoInputsPassed()
        {
            // arrange

            // act
            var results = _dataService.GetData();

            // assert
            Assert.Equal(2, results.Count());
        }
    }
}
