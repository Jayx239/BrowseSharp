using System.Threading.Tasks;

namespace BrowseSharp
{
    public interface IScraper
    {
        int Add(Document document);
        Task<int> AddAsync(Document document);
    }
}