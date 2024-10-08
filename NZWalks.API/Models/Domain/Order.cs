﻿namespace NZWalks.API.Models.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public double? GrandTotal { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UserId { get; set; }
        public User User { get; set; } = default!;
        public virtual ICollection<OrderItem> OrderItems { get; set; } = default!;
    }
}
