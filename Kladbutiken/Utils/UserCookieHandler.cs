using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace Kladbutiken.Utils
{
    public static class UserCookieHandler
    {
        public static async Task<User> GetUserAndCartByCookies(string userCookie, string cartCookie)
        {
            var user = await GetUserByCookie(userCookie);

            if (user is null)
            {
                return null;
            }

            if (cartCookie is null)
            {
                return user;
            }

            var productCart = await GetProductCartByCookie(cartCookie);

            if (productCart != null)
            {
                user.ProductCart = productCart;
            }

            return user;
        }

        public static async Task<User> GetUserByCookie(string userCookie)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var client = new HttpClient { BaseAddress = new Uri("https://localhost:44331/api") };

            var userResponse = await client.GetStringAsync(client.BaseAddress + "/user/" + userCookie);
            var user = JsonSerializer.Deserialize<User>(userResponse, jsonOptions);

            return user;
        }

        public static async Task<List<Product>> GetProductCartByCookie(string cartCookie)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var client = new HttpClient { BaseAddress = new Uri("https://localhost:44331/api") };

            if (cartCookie is null)
            {
                return null;
            }

            var deserializedCart = JsonSerializer.Deserialize<List<Guid>>(cartCookie, jsonOptions);
            HttpContent content = JsonContent.Create(deserializedCart);
            var cartResponse = await client.PostAsync(client.BaseAddress + "/product/cart", content);

            if (!cartResponse.IsSuccessStatusCode)
            {
                return null;
            }

            return await cartResponse.Content.ReadFromJsonAsync<List<Product>>();
        }
    }
}
