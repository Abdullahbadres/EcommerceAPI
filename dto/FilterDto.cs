public class FilterDto
{
    public string? Name { get; set; } //nullable biar bisa null kalo ga diisi
    public decimal? MinPrice { get; set; } //nullable biar bisa null kalo ga diisi
    public decimal? MaxPrice { get; set; } //nullable biar bisa null kalo ga diisi
    public string? SortBy { get; set; } //nullable biar bisa null kalo ga diisi, contoh value: price_asc, price_desc
    public int PageNumber { get; set; } = 1; //default page number 1
    public int PageSize { get; set; } = 10; //default page size 10
}