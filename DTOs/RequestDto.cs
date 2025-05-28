namespace InventoryManagementAPI.DTOs
{
    public class RequestDto
    {
        public string? Name { get; set; }
        public string? Category { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool ShowLowStockOnly { get; set; }

        public string? SortBy { get; set; } = "Name";
        public bool SortDescending { get; set; } = false;

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}