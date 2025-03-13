using Automobile.Application.Models;

namespace Automobile.Application.Data
{
    public interface IDataService
    {
        IEnumerable<Vehicle> GetData();
    }
}
