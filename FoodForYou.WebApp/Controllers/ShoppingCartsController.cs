using System;
using System.Threading.Tasks;
using FoodForYou.Core.Application.ServiceContracts;
using FoodForYou.Core.Models.Relational;
using FoodForYou.Core.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodForYou.WebApp.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IUserService _userService;
        private readonly IShoppingService _shoppingService;
        private readonly IProductService _productService;

        public ShoppingCartsController(IUserService userService, IShoppingService shoppingService, IProductService productService)
        {
            _userService = userService;
            _shoppingService = shoppingService;
            _productService = productService;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            var shoppingCart = await GetCartFromContext(HttpContext);
            if (shoppingCart != null)
            {
                return View(shoppingCart);
            }
            return View("Empty");
        }
        
        public async Task<IActionResult> AddProduct(int id)
        {
            var shoppingCart = await GetCartFromContext(HttpContext);
            if (shoppingCart != null)
            {
                var addedProduct = await _productService.GetId(id);
                _shoppingService.AddProduct(await GetUserFromContext(HttpContext), addedProduct);
                return View("Index", shoppingCart);
            }
            
            return RedirectToAction("Index", "Products");
        }
        
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var shoppingCart = await GetCartFromContext(HttpContext);
            if (shoppingCart != null)
            {
                var removedProduct = await _productService.GetId(id);
                _shoppingService.RemoveProduct(await GetUserFromContext(HttpContext), removedProduct);
                return View("Index", shoppingCart);
            }
            
            return RedirectToAction("Index", "Products");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeQuantity([FromForm][Bind("Product.ProductId,Quantity")] OrderProduct orderProduct)
        {
            var shoppingCart = await GetCartFromContext(HttpContext);
            if (shoppingCart != null)
            {
                var cartProduct = await _productService.GetId(orderProduct.Product.ProductId);
                _shoppingService.EditProductQuantity(await GetUserFromContext(HttpContext), cartProduct, orderProduct.Quantity);
                return View("Index", shoppingCart);
            }
            
            return RedirectToAction("Index", "Products");
        }

        private async Task<CostumerCart> GetCartFromContext(HttpContext context)
        {
            var user = await GetUserFromContext(context);
            var shoppingCart = _shoppingService.Get(user) ?? _shoppingService.Create(user, new CostumerCart(user));
            return shoppingCart;
        }
        
        private async Task<User> GetUserFromContext(HttpContext context)
        {
            var guidStr = context.Session.GetString("_userId");
            if (string.IsNullOrEmpty(guidStr))
            {
                var newGuid = Guid.NewGuid();
                context.Session.SetString("_userId", newGuid.ToString());
                await context.Session.CommitAsync();
                return new User(){UserId = newGuid, Registered = false};
            }
            var guid = new Guid(guidStr);
            var user = await _userService.GetId(guid);
            return user ?? new User(){UserId = guid, Registered = false};
        }
    }
}