﻿namespace NZWalks.API.Models.DTO
{
    public class UpdateProductRequestDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Sku { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
    }
}
