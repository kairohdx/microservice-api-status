public class ApiStatus
{
    public int ReturnStatus { get; set; }
    public float Ms { get; set; }
    public DateTime Timestamp { get; set; }
    public required string StatusDescription { get; set; }
}
