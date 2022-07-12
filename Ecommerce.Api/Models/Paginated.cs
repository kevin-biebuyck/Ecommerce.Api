namespace Ecommerce.Api.Models
{
    public class Paginated<TItem>
    {
        public int Page { get; }
        public int PageSize { get; }
        public int TotalPages { get; }
        public TItem[] Items { get; }

        public Paginated(int page, int pageSize, int totalPages, TItem[] items)
        {
            Page = page;
            PageSize = pageSize;
            TotalPages = totalPages;
            Items = items;
        }
    }
}
