namespace VinylBack.DTOs
{
    public class PurchaseDTO
    {
        public int PurchaseId { get; set; }
        public int UserId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double TotalAmount { get; set; }
        public int StatusId { get; set; }
        public int LocationId { get; set; }
    }
}
