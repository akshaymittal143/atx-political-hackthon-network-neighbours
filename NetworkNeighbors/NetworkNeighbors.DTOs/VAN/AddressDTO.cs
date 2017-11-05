namespace NetworkNeighbors.DTOs.VAN
{
    public class AddressDTO
    {
        public int addressId { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string addressLine3 { get; set; }
        public string city { get; set; }
        public string stateOrProvince { get; set; }
        public string zipOrPostalCode { get; set; }
        public string countryCode { get; set; }
        public string type { get; set; }
        public bool isPreferred { get; set; }
    }
}