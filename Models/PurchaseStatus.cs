namespace VinylBack.Models
{
    public class PurchaseStatus
    {
        public int PurchaseStatusId {  get; set; }
        public string PurchaseStatusName { get; set; }
        public ICollection<Purchase> Purchases { get; set; }

    }
}
