public class SocketConfig
{
    public int SocketIndex { get; set; }
    public int Port { get; set; }
    public int CheckIntervalMinutes { get; set; }
    public PriorityLevel PriorityLevel { get; set; }
    public bool ObserverEnabled { get; set; }
    public DateTime LastExecutionDate { get; set; }
}
