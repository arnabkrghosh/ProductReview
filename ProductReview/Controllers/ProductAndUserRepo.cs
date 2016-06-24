using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProductReview.Models;

namespace ProductReview.Controllers
{
    public sealed class ProductAndUserRepo
    {
        // following const are public for unit testing
        public const string ProductId1 = "ProductId1";

        public const string ProductId2 = "ProductId2";

        public const string ProductId3 = "ProductId3";

        public const string UserId1 = "UserId1";

        public const string UserId2 = "UserId2";

        public const string UserId3 = "UserId3";

        public static Dictionary<string, Product> ProductList;

        public static Dictionary<string, User> UserList;

        static ProductAndUserRepo()
        {
            ProductList = new Dictionary<string, Product>();
            ProductList.Add(ProductId1, new Product(ProductId1, "Product1"));
            ProductList.Add(ProductId2, new Product(ProductId2, "Product2"));
            ProductList.Add(ProductId3, new Product(ProductId3, "Product3"));

            UserList = new Dictionary<string, User>();
            UserList.Add(UserId1, new User(UserId1, "User1"));
            UserList.Add(UserId2, new User(UserId2, "User2"));
            UserList.Add(UserId3, new User(UserId3, "User3"));
        }

    }
}