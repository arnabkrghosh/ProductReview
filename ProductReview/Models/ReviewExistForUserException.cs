using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductReview.Models
{
    public sealed class ReviewExistForUserException : ApplicationException
    {
        public ReviewExistForUserException(string message): base(message)
        {
        }
    }
}