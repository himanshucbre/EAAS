namespace EaaSAPI.Models
{
    public class RequestModel
    {
        public string type { get; set; }
        public string text { get; set; }
        public byte[] bytes { get; set; }
        public string key { get; set; }
    }
}
