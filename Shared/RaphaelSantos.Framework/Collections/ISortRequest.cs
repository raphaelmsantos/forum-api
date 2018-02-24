namespace RaphaelSantos.Framework.Collections
{
    public interface ISortRequest
    {
        string SortField { get; set; }

        bool SortDescending { get; set; }
    }
}
