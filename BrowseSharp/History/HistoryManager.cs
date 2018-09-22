using Jint.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseSharp.History
{
    public class HistoryManager : IHistory
    {
        #region Constructors
        public HistoryManager()
        {
            _history = new List<IDocument>();
            _forwardHistory = new List<IDocument>();
            MaxHistorySize = -1;
        }

        public HistoryManager(List<IDocument> history, List<IDocument> forwardHistory)
        {
            _history = history;
            _forwardHistory = forwardHistory;
            MaxHistorySize = -1;
        }

        public HistoryManager(List<IDocument> history, List<IDocument> forwardHistory, int maxHistorySize)
        {
            _history = history;
            _forwardHistory = forwardHistory;
            MaxHistorySize = maxHistorySize;
        }

        #endregion

        #region Public Attributes
        public List<IDocument> History { get { return _history; } }
        public List<IDocument> ForwardHistory { get { return _forwardHistory; } }
        public int MaxHistorySize { get; set; }
        #endregion

        #region Private Attributes

        private List<IDocument> _history;
        private List<IDocument> _forwardHistory;

        #endregion

        #region Public Methods

        public IDocument Back()
        {
            return Back(false);
        }

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

        public IDocument Forward()
        {
            return Forward(false);
        }

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

        public IDocument Refresh()
        {
            return History.Pop();
        }

        public IDocument Document { get { return History.Last(); } }
        #endregion

    }
}
