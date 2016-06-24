using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductReview.Models
{
    public sealed class User
    {
        public string Name { get; set; }

        // Intended to be used for authz - not implemented - only user who created review can edit it 
        public string Id { get; private set; }

        public User(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}