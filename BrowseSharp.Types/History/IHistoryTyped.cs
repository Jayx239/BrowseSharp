using System.Threading.Tasks;

namespace BrowseSharp.History
{
    /// <summary>
    /// Browse history management methods with type support
    /// </summary>
    public interface IHistoryTyped
    {
        /// <summary>
        /// Clears browse history by re-initializing Documents
        /// </summary>
        void ClearHistory();

        /// <summary>
        /// Clears forward history
        /// </summary>
        void ClearForwardHistory();

        /// <summary>
        /// Method for navigating to last browser state by re-issuing the previous request
        /// </summary>
        IDocument<T> Back<T>();

        /// <summary>
        /// Method for navigating to last browser state
        /// </summary>
        /// <param name="useCache">Determines whether to re-issue request or reload last document</param>
        IDocument<T> Back<T>(bool useCache);

        /// <summary>
        /// Method for navigating to last browser state by re-issuing the previous request asynchronously
        /// </summary>
        Task<IDocument<T>> BackAsync<T>();

        /// <summary>
        /// Method for navigating to last browser state asynchronously
        /// </summary>
        /// <param name="useCache">Determines whether to re-issue request or reload last document</param>
        Task<IDocument<T>> BackAsync<T>(bool useCache);

        /// <summary>
        /// Navigate to next document in forward history
        /// </summary>
        /// <returns></returns>
        IDocument<T> Forward<T>();

        /// <summary>
        /// Navigate to next document in forward history
        /// </summary>
        /// <param name="useCache"></param>
        /// <returns></returns>
        IDocument<T> Forward<T>(bool useCache);

        /// <summary>
        /// Navigate to next document in forward history asynchronously
        /// </summary>
        /// <returns></returns>
        Task<IDocument<T>> ForwardAsync<T>();

        /// <summary>
        /// Navigate to next document in forward history asynchronously
        /// </summary>
        /// <param name="useCache"></param>
        /// <returns></returns>
        Task<IDocument<T>> ForwardAsync<T>(bool useCache);

        /// <summary>
        /// Max amount of history/Documents to be stored by the browser (-1 for no limit)
        /// </summary>
        int MaxHistorySize { get; set; }

        /// <summary>
        /// Refresh page, re-submits last request
        /// </summary>
        /// <returns></returns>
        IDocument<T> Refresh<T>();

        /// <summary>
        /// Refresh page, re-submits last request asynchronously
        /// </summary>
        /// <returns></returns>
        Task<IDocument<T>> RefreshAsync<T>();

    }
}
