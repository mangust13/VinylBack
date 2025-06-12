using System.ComponentModel.DataAnnotations;

namespace VinylBack.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }

        public ICollection<Basket> Baskets { get; set; }
        public ICollection<Purchase> Purchases { get; set; }

    }
}
