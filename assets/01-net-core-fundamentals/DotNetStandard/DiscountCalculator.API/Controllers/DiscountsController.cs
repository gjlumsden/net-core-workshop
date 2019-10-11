using System;
using DiscountCalculator.Core;
using Microsoft.AspNetCore.Mvc;

namespace DiscountCalculator.API.Controllers
{
    [Route("api/discounts")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<double> Get(DateTime dateJoined)
        {
            var engine = new DiscountEngine();
            var discount = engine.GetDiscountAmount(dateJoined);
            return discount;
        }
    }
}