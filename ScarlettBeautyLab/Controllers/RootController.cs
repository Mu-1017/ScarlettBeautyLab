using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ScarlettBeautyLab.Controllers
{
    [Route("api/")]
    [ApiController]
    [ApiVersion("1.0")]
    public class RootController : ControllerBase
    {
        // GET: api/
        [HttpGet(Name = nameof(GetRoot))]
        [ProducesResponseType(200)]
        public IActionResult GetRoot()
        {
            var response = new
            {
                href = Url.Link(nameof(GetRoot), null),
                info = new
                {
                    href = Url.Link(nameof(InfoController.GetInfo), null)
                }
            };

            return Ok(response);
        }
    }
}
