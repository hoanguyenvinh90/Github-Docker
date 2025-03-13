using Automobile.Application.Models;

namespace Automobile.Application.Data
{
    public sealed class DataService : IDataService
    {
        public IEnumerable<Vehicle> GetData() => new List<Vehicle>
            {
                new Vehicle
                {
                    Id = 1,
                    Name = "Toyota Camry LE 2019"
                },
                new Vehicle
                {
                    Id = 2,
                    Name = "Mercedes C300"
                }
            };
    }
}
