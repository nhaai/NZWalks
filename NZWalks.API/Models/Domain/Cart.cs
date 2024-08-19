namespace NZWalks.API.Models.Domain
{
    public class Cart
    {
        public int Id { get; set; }
        public double? GrandTotal { get; set; }
        public string? UserId { get; set; }
        public User User { get; set; } = default!;
        public virtual ICollection<CartLine> CartLines { get; set; } = default!;
    }
}
