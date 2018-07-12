using System.Threading.Tasks;

namespace BrowseSharp
{
    public interface IScraper
    {
        int Add(IDocument document);
        Task<int> AddAsync(IDocument document);
    }
}