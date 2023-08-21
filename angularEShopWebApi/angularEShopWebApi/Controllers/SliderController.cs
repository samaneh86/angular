using AngularEShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularEShop.Core.Utilities.Common;
namespace angularEShopWebApi.Controllers
{
  
    public class SliderController :SiteBaseController
    {
        private ISliderService sliderService;
        public SliderController(ISliderService sliderService)
        {
            this.sliderService = sliderService;
        }

        
        [HttpGet("get-active-sliders")]
        public async Task<IActionResult> GetActiveSliders()
        {
           
            return JsonResponse.Success(await sliderService.GetActiveSliders());
        }
    }
}
