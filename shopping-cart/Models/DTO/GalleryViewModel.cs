
using SA51_CA_Project_Team10.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace SA51_CA_Project_Team10.Models.DTO
{
    public class GalleryViewModel
    {
        public int Columns { get; private set; }
        public int Page { get; private set; }
        public int TotalProducts { get; private set; }
        public int TotalPage { get; private set; }
        public List<ProductDto> DisplayedProducts { get; private set; }
        public User User { get; private set; }

        // Row and column here refer to the number of products that should be visible in a row and column respectively
        // Example: row = 3, column = 4 means that there should be 3 rows and 4 columns total in a page (12 products total)
        public GalleryViewModel(User user, int page, StaticPagedList<ProductDto> productsPagedList)
        {
            Columns = productsPagedList.PageSize; // Assuming that Columns represent page size here
            Page = page;
            TotalProducts = productsPagedList.TotalItemCount;
            TotalPage = productsPagedList.PageCount;
            DisplayedProducts = productsPagedList.ToList();
            User = user;
        }

        //public GalleryViewModel(User user, int page, List<Product> products) : this(4, 3, user, page, products) { }

        public double AverageRating(int productId)
        {
            //Product p = DisplayedProducts.FirstOrDefault(x => x.Id == productId);
            double nearestHalf = 0;

            //if (p.Ratings.Count == 0)
            //{
            //    nearestHalf = 0;
            //} else
            //{
            //    double trueAverage = p.Ratings.Average(x => x.Score);
            //    nearestHalf = Math.Round(trueAverage * 2) / 2;
            //}

            return nearestHalf;
        }

        public int UserRating(int productId) {
            int userRating = 0;
            //if (User == null || User.Ratings.FirstOrDefault(x => x.ProductId == productId) == null)
            //{
            //    userRating = 0;
            //} else
            //{
            //    userRating = User.Ratings.FirstOrDefault(x => x.ProductId == productId).Score;
            //}
            return userRating;
        }
    }
}
