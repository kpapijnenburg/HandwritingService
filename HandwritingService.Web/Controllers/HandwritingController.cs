﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandwritingService.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandwritingController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello, World!");
        }
    }
}