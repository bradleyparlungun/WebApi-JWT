namespace alxbrn_api.Models
{
    /// <summary>
    /// Class for storing PagingParameterObjects
    /// </summary>
    public class PagingParameterModel
    {
        /// <summary>
        /// MaxPageSize of the specific PagingParameterModel
        /// </summary>
        private const int MaxPageSize = 20;

        /// <summary>
        /// PageNumber of the specific PagingParameterModel
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// _pageSize of the specific PagingParameterModel
        /// </summary>
        private int _pageSize { get; set; }

        /// <summary>
        /// PageSize of the specific PagingParameterModel
        /// </summary>
        public int PageSize
        {

            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        /// <summary>
        /// QuerySearch of the specific PagingParameterModel
        /// </summary>
        public string QuerySearch { get; set; }
    }
}