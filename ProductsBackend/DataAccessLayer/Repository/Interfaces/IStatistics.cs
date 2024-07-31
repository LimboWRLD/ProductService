using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IStatistics
    {
        public Task<Dictionary<string, string>> GetBasicStatistics();

        public Task<IEnumerable<Dictionary<string, string>>> GetMostPopular(int? range);
    }
}
