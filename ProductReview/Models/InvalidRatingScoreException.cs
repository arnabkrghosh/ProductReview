using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductReview.Models
{
    public sealed class InvalidRatingScoreException : ApplicationException
    {
        public InvalidRatingScoreException(string message) : base(message)
        {
        }
    }
}