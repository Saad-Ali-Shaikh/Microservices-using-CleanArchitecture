using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            this.redisCache = redisCache;
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await redisCache.GetStringAsync(userName);
            //var shoppingCart = new ShoppingCart();
            if (string.IsNullOrEmpty(basket))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart)
        {
            await redisCache.SetStringAsync(shoppingCart.UserName, JsonConvert.SerializeObject(shoppingCart));
            return await GetBasket(shoppingCart.UserName);

        }
        public async Task DeleteBasket(string userName)
        {
            await redisCache.RemoveAsync(userName);
        }

    }
}
