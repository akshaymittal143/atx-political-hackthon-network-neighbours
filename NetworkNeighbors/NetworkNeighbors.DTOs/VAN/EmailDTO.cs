namespace NetworkNeighbors.DTOs.VAN
{
    public class EmailDTO
    {
        public string email { get; set; }
        public string type { get; set; }
        public bool isPreferred { get; set; }
        public bool isSubscribed { get; set; } // NPG VAN has this as nullable
    }
}