namespace RaphaelSantos.Framework.Collections
{
    /// <summary>
    /// Page Information
    /// </summary>
    public interface IPagedList : IPageRequest
    {
        /// <summary>
        /// Page count
        /// </summary>
        int PageCount { get; }

        /// <summary>
        /// Recourd count
        /// </summary>
        int RecordCount { get; set; }
    }
}
