using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScarlettBeautyLab.Filters
{
    public class RequireHttpsOrCloseAttribute: RequireHttpsAttribute
    {
        protected override void HandleNonHttpsRequest(AuthorizationFilterContext filterContext)
        {
            //Tell the user they made a bad request if they accidently sent a http request
            filterContext.Result = new StatusCodeResult(400);
        }
    }
}
