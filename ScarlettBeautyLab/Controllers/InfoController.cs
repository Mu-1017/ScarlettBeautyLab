using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ScarlettBeautyLab.Models;

namespace ScarlettBeautyLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly OwnerInfo _ownerInfo;

        public InfoController(IOptions<OwnerInfo> ownerInfoWrapper)
        {
            _ownerInfo = ownerInfoWrapper.Value;
        }
        [HttpGet(Name = nameof(GetInfo))]
        [ProducesResponseType(200)]
        public ActionResult<OwnerInfo> GetInfo()
        {
            _ownerInfo.Href = Url.Link(nameof(GetInfo), null);
            return _ownerInfo;
        }
    }
}