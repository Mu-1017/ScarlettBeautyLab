using System;
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
        [HttpGet(Name = nameof(GetAllBrands))]
        public ActionResult<List<BrandEntity>> GetAllBrands(BeautyLabDbContext context)
        {
            var brands = context.Brands.ToList();

            return brands;
        }
    }
}