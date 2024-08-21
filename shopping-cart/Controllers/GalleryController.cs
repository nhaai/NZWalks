using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SA51_CA_Project_Team10.DBs;
using SA51_CA_Project_Team10.Models.Domain;
using SA51_CA_Project_Team10.Models.DTO;
using SA51_CA_Project_Team10.Repositories;
using X.PagedList;

namespace SA51_CA_Project_Team10.Controllers
{
    public class GalleryController : Controller
    {
        private readonly Verify _v;
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly ILogger<GalleryController> logger;
        public GalleryController( Verify v, IMapper mapper, IProductRepository productRepository, ILogger<GalleryController> logger) {
            _v = v;
            this.mapper = mapper;
            this.productRepository = productRepository;
            this.logger = logger;
        }
        public async Task<IActionResult> IndexAsync([FromQuery] SearchFilterDto searchFilterDto)
        {
            if (searchFilterDto == null)
            {
                searchFilterDto = new SearchFilterDto { PageNumber = 1, PageSize = 10 };
            }

            User user = null; // If user data is available, populate this variable accordingly
            searchFilterDto.FilterOn = "Category";  // Set the field you want to filter on
            searchFilterDto.FilterQuery = "Books";  // Set the filter query
            searchFilterDto.SortBy = "Name";        // Set the sorting field
            searchFilterDto.IsAccending = true;     // Set the sorting direction
            searchFilterDto.PageNumber = 1;         // Set the page number
            searchFilterDto.PageSize = 10;          // Set the page size
            var searchDomainModel = mapper.Map<SearchFilter>(searchFilterDto);
            if (searchDomainModel == null)
            {
                // Handle null mapping
                return BadRequest("Invalid search parameters.");
            }
            (var productsDomainModel, int totalItemCount) = await productRepository.GetAllAsync(searchDomainModel);
            if (productsDomainModel == null || totalItemCount == 0)
            {
                // Handle empty or null products
                return View("NoProducts"); // A view to show when no products are found
            }

            var productsDto = mapper.Map<List<ProductDto>>(productsDomainModel);
            if (productsDto == null)
            {
                // Handle null mapping
                return BadRequest("Error mapping products.");
            }

            var productsPagedList = new StaticPagedList<ProductDto>(productsDto, searchFilterDto.PageNumber, searchFilterDto.PageSize, totalItemCount);
            if (productsPagedList == null)
            {
                // Handle null PagedList
                return BadRequest("Error creating product list.");
            }

            var galleryView = new GalleryViewModel(user, searchFilterDto.PageNumber, productsPagedList);

            ViewData["Searchbar"] = searchFilterDto.SearchTerm ?? string.Empty;

            return View(galleryView);
        }


        [HttpPost]
        public IActionResult AddCart(int productId) {
            string sessionId = HttpContext.Request.Cookies["sessionId"];

            //if (_v.VerifySession(sessionId, _db))
            //{
            //    int userid = _db.Sessions.FirstOrDefault(x => x.Id == sessionId).UserId;
            //    Cart cart = _db.Carts.FirstOrDefault(x => x.UserId == userid && x.ProductId == productId);

            //    // Special case handling to prevent integer overflow
            //    if (cart == null)
            //    {
            //        _db.Add(new Cart()
            //        {
            //            Quantity = 1,
            //            UserId = userid,
            //            ProductId = productId
            //        });
            //    }
            //    else if (cart.Quantity < 100)
            //    {
            //        cart.Quantity += 1;
            //    } else
            //    {
            //        TempData["Alert"] = "warning|Cannot have more than 100 of the same product at once in cart, please contact Team 10 for bulk purchases.";
            //        return Json(new
            //        {
            //            success = false,
            //        });
            //    }

            //    _db.SaveChanges();

            //    return Json(new
            //    {
            //        success = true
            //    });
            //} else 
            //{
            //    string cartCookie = HttpContext.Request.Cookies["guestCart"];
            //    GuestCart guestCart;
            //    if (cartCookie != null)
            //    {
            //        guestCart = JsonSerializer.Deserialize<GuestCart>(cartCookie);
            //    }
            //    else
            //    {
            //        guestCart = new GuestCart();
            //        guestCart.Add(productId, _db.Products.FirstOrDefault(p => p.Id == productId));
            //    }

            //    Product product = _db.Products.FirstOrDefault(p => p.Id == productId);
            //    Cart inCart = guestCart.Find(productId);

            //    // Special case handling to prevent integer overflow
            //    if (inCart == null || inCart.Quantity < 100)
            //    {
            //        guestCart.Add(productId, product);
            //    } else
            //    {
            //        TempData["Alert"] = "warning|Cannot have more than 100 of the same product at once in cart, please contact Team 10 for bulk purchases.";
            //        return Json(new
            //        {
            //            success = false,
            //        });
            //    }

            //    HttpContext.Response.Cookies.Append("guestCart", JsonSerializer.Serialize<GuestCart>(guestCart), new CookieOptions
            //    {
            //        HttpOnly = true,
            //        SameSite = SameSiteMode.Lax
            //    });

            return Json(new
            {
                success = true
            });
            //}
        }

        [HttpPost]
        public IActionResult Rate(int rating, int productId)
        {
            string sessionId = HttpContext.Request.Cookies["sessionId"];

            //if (_v.VerifySession(sessionId, _db))
            //{
            //    User user = _db.Sessions.FirstOrDefault(session => session.Id == sessionId).User;

            //    //_db.Ratings.Add(new Rating
            //    //{
            //    //    UserId = user.Id,
            //    //    ProductId = productId,
            //    //    Score = rating
            //    //});

            //    _db.SaveChanges();

            //    return Json(new
            //    {
            //        success = true,
            //        newAverage = Math.Round(_db.Ratings.Where(rating => rating.ProductId == productId).Average(rating => rating.Score) * 2) / 2
            //    });
            //}
            //else
            //{
            //    TempData["Alert"] = "primary|Please log in to rate products.";
            return Json(new
            {
                //        success = false,
                //        redirect = "/Login/Index"
            });
            //}
        }
    }
}
