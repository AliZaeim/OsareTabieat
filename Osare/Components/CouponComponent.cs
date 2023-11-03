﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NatureExtract_Web.Components
{
    public class CouponComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View("/Pages/Components/_GetCoupon.cshtml"));
        }
    }
}
