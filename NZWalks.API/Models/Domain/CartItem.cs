namespace NZWalks.API.Models.Domain
{
    public class CartItem
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public int ProductId {  get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
