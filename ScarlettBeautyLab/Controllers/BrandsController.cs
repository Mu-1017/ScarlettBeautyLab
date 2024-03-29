﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScarlettBeautyLab.Models;

namespace ScarlettBeautyLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly BeautyLabDbContext _context;

        public BrandsController(BeautyLabDbContext context)
        {
            this._context = context;
        }
        [HttpGet(Name = nameof(GetAllBrands))]
        public ActionResult<List<BrandEntity>> GetAllBrands()
        {
            var brands = _context.Brands.ToList();

            return brands;
        }
    }
}