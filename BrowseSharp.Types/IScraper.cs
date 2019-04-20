using System.Threading.Tasks;

namespace BrowseSharp.Types
{
    /// <summary>
    /// Interface for scrapers
    /// </summary>
    public interface IScraper
    {
        /// <summary>
        /// Method for adding scraped elements to a document
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        int Add(IDocument document);
        
        /// <summary>
        /// Method for adding scraped elements to a document asynchronously
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        Task<int> AddAsync(IDocument document);
    }
}