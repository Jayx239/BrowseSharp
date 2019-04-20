using BrowseSharp.Types.Toolbox;
using System.Collections.Generic;
using System.Linq;

namespace BrowseSharp.History
{
    /// <summary>
    /// Class for managing browser history
    /// </summary>
    public class HistoryManager : IHistorySync
    {
        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public HistoryManager()
        {
            _history = new List<IDocument>();
            _forwardHistory = new List<IDocument>();
            MaxHistorySize = -1;
        }
        /// <summary>
        /// Secondary constructor for intializing with history and forward history
        /// </summary>
        /// <param name="history">Browser history</param>
        /// <param name="forwardHistory">Forward browser history</param>

        public HistoryManager(List<IDocument> history, List<IDocument> forwardHistory)
        {
            _history = history;
            _forwardHistory = forwardHistory;
            MaxHistorySize = -1;
        }

        /// <summary>
        /// Secondary constructor for initializing with history, forward history, and max history size
        /// </summary>
        /// <param name="history">Browser history</param>
        /// <param name="forwardHistory">Forward browser history</param>
        /// <param name="maxHistorySize">Maximum history size</param>
        public HistoryManager(List<IDocument> history, List<IDocument> forwardHistory, int maxHistorySize)
        {
            _history = history;
            _forwardHistory = forwardHistory;
            MaxHistorySize = maxHistorySize;
        }

        #endregion

        #region Public Attributes
        /// <summary>
        /// List of documents in history
        /// </summary>
        public List<IDocument> History { get { return _history; } }
        /// <summary>
        /// List of forward history
        /// </summary>
        public List<IDocument> ForwardHistory { get { return _forwardHistory; } }
        /// <summary>
        /// Maximum history size
        /// </summary>
        public int MaxHistorySize { get; set; }
        #endregion

        #region Private Attributes
        /// <summary>
        /// History documents
        /// </summary>
        private List<IDocument> _history;
        /// <summary>
        /// Forward history documents
        /// </summary>
        private List<IDocument> _forwardHistory;

        #endregion

        #region Public Methods

        /// <summary>
        /// Navigate back to previous history document without cache
        /// </summary>
        /// <returns></returns>
        public IDocument Back()
        {
            return Back(false);
        }
        /// <inheritdoc/>
        public IDocument Back(bool useCache)
        {
            ForwardHistory.Push(History.Pop());
            if (useCache)
                return History.Last();
            IDocument oldDocument = History.Pop();
            return oldDocument;
        }

        /// <summary>
        /// Clears forward history
        /// </summary>
        public void ClearForwardHistory()
        {
            if (_forwardHistory == null)
            {
                _forwardHistory = new List<IDocument>();
                return;
            }

            if (_forwardHistory.Count > 0)
            {
                _forwardHistory.Clear();
            }
        }

        /// <inheritdoc/>
        public void ClearHistory()
        {
            if (_history == null)
            {
                _history = new List<IDocument>();
                return;
            }

            if (_history.Count > 0)
            {
                _history.Clear();
            }
            
            ClearForwardHistory();
        }

        /// <inheritdoc/>
        public IDocument Forward()
        {
            return Forward(false);
        }

        /// <inheritdoc/>
        public IDocument Forward(bool useCache)
        {
            if (_forwardHistory.Count < 1)
                return History.LastOrDefault();

            if (useCache)
            {
                History.Push(_forwardHistory.Pop());
                return Document;
            }

            IDocument forwardDocument = _forwardHistory.Pop();
            return forwardDocument;
        }

        /// <inheritdoc/>
        public IDocument Refresh()
        {
            return History.Pop();
        }

        /// <summary>
        /// Current document
        /// </summary>
        public IDocument Document { get { return History.Last(); } }

        /// <summary>
        /// Manages history when browser performs a navigate method
        /// </summary>
        public void Navigate()
        {
            ClearForwardHistory();
            TrimHistory(true);
        }

        /// <summary>
        /// Manages history when browser performs a submit method
        /// </summary>
        public void Submit()
        {
            Navigate();
        }

        /// <summary>
        /// Trims history to the max history size
        /// </summary>
        /// <param name="beforeNavigate">Indicates whether method is called before or after navigation</param>
        protected void TrimHistory(bool beforeNavigate)
        {
            if (MaxHistorySize < 0)
                return;

            if (beforeNavigate)
            {
                while (History.Count >= MaxHistorySize)
                    History.RemoveAt(0);
            }
            else
            {
                while (History.Count > MaxHistorySize)
                    History.RemoveAt(0);
            }
        }

        #endregion

    }
}
