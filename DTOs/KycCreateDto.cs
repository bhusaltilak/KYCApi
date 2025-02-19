namespace KycApi.DTOs
{
    public class KycCreateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string VDC { get; set; }
    }
}
