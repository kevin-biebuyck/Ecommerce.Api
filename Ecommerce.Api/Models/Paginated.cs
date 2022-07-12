namespace Ecommerce.Api.Models
{
    public class Paginated<TItem>
    {
        public int Page { get; }
        public int PageSize { get; }
        public int TotalItems { get; }
        public TItem[] Items { get; }

        public Paginated(int page, int pageSize, int totalItems, TItem[] items)
        {
            Page = page;
            PageSize = pageSize;
            TotalItems = totalItems;
            Items = items;
        }
    }
}
