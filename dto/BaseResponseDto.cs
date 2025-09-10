public class BaseResponseDto<T> //<T> ini generic biar bisa dipake di response dto mana aja
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; } //ini nullable biar bisa null kalo misal data ga ada
    public object? Errors { get; set; } //ini nullable biar bisa null kalo misal ga ada error
    public DateTime Timestamp { get; set; } = DateTime.UtcNow; //ini buat nyimpen waktu response dibuat
}