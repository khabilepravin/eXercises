﻿using Microsoft.AspNetCore.Mvc;

namespace eXercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController()
        {


        }

        [HttpGet]
        public ActionResult GetProduct()
        {
            return Ok("Hello");
        }
    }
}