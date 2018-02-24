namespace RaphaelSantos.Framework.Collections
{
    public interface IPageRequest
    {
        int PageNumber { get; set; }

        int PageSize { get; set; }
    }
}
