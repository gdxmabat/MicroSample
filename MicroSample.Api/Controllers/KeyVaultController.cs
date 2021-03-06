﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MicroSample.Api.Controllers
{
    [Route("api/[controller]")]
    public class KeyVaultController : Controller
    {
        private readonly IConfiguration configuration;

        public KeyVaultController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
           
            return this.Json(new
            {
                websecret = this.configuration.GetValue<string>("websecret")
            });
        }
    }
}