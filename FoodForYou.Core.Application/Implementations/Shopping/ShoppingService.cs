using System;
using System.Linq;
using System.Threading;
using FoodForYou.Core.Application.ServiceContracts;
using FoodForYou.Core.Models.Relational;
using Microsoft.Extensions.Caching.Memory;

namespace FoodForYou.Core.Application.Implementations.Shopping
{
    public class ShoppingService : IShoppingService
    {
        protected readonly IMemoryCache Cache;

        protected readonly TimeSpan Ttl = TimeSpan.FromDays(3);

        public ShoppingService(IMemoryCache cache)
        {
            Cache = cache;
        }

        public CostumerCart Get(Models.Users.User user, CancellationToken cancellationToken = default)
        {
            return Cache.Get<CostumerCart>(user.UserId);
        }

        public CostumerCart Create(Models.Users.User user, CostumerCart model, CancellationToken cancellationToken = default)
        {
            Cache.CreateEntry(user.UserId);
            Cache.Set(user.UserId, model, Ttl);
            return model;
        }

        public CostumerCart Edit(Models.Users.User user, CostumerCart model, CancellationToken cancellationToken = default)
        {
            var toEdit = Cache.Get<CostumerCart>(user.UserId);

            Cache.Set(user.UserId, Map(toEdit, model));

            return model;
        }

        public void Delete(Models.Users.User user, CancellationToken cancellationToken = default)
        {
            var toEdit = Cache.Get<CostumerCart>(user.UserId);
            Cache.Remove(toEdit);
        }

        public bool Exists(Models.Users.User user, CancellationToken cancellationToken = default)
        {
            return Cache.TryGetValue<CostumerCart>(user.UserId, out _);
        }

        public CostumerCart AddProduct(Models.Users.User user, Models.Products.Product product, CancellationToken cancellationToken = default)
        {
            var toEdit = Cache.Get<CostumerCart>(user.UserId);

            var foundProduct = toEdit.CartProducts.FirstOrDefault(x => x.Product.ProductId == product.ProductId);
            if (foundProduct != null)
            {
                ++foundProduct.Quantity;
            }
            else
            {
                toEdit.CartProducts.Add(new OrderProduct(){Product = product, Quantity = 1});
            }
            
            Cache.Set(user.UserId, toEdit);

            return toEdit;
        }

        public CostumerCart RemoveProduct(Models.Users.User user, Models.Products.Product product, CancellationToken cancellationToken = default)
        {
            var toEdit = Cache.Get<CostumerCart>(user.UserId);

            var foundProduct = toEdit.CartProducts.FirstOrDefault(x => x.Product.ProductId == product.ProductId);
            if (foundProduct != null)
            {
                if (foundProduct.Quantity - 1 > 0)
                {
                    --foundProduct.Quantity;
                }
                else
                {
                    toEdit.CartProducts.Remove(foundProduct);
                }
            }
            
            Cache.Set(user.UserId, toEdit);

            return toEdit;
        }

        public CostumerCart EditProductQuantity(Models.Users.User user, Models.Products.Product product, int quantity, CancellationToken cancellationToken = default)
        {
            var toEdit = Cache.Get<CostumerCart>(user.UserId);

            var foundProduct = toEdit.CartProducts.FirstOrDefault(x => x.Product.ProductId == product.ProductId);
            if (foundProduct != null)
            {
                if (quantity > 0)
                {
                    foundProduct.Quantity = quantity;
                }
                else
                {
                    toEdit.CartProducts.Remove(foundProduct);
                }
            }
            
            Cache.Set(user.UserId, toEdit);

            return toEdit;
        }
        
        
        private static CostumerCart Map(CostumerCart costumerCart, CostumerCart updatedCostumerCart)
        {
            costumerCart.Costumer = updatedCostumerCart.Costumer;
            costumerCart.Shipper = updatedCostumerCart.Shipper;
            costumerCart.CartProducts = updatedCostumerCart.CartProducts;

            return costumerCart;
        }
    }
}