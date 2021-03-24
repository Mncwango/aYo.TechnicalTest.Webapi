using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aYo.TechnicalTest.Models;
using aYo.TechnicalTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aYo.TechnicalTest.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversionController : ControllerBase
        
    {
        private readonly IConversionService _conversionService;
        public ConversionController(IConversionService conversionService)
        {
            this._conversionService = conversionService;
        }

        [HttpGet]
        public async Task<IActionResult> Convert(int input, ConversionType converstionType, ConversionUnits conversionUnits)
        {
            return Ok(await this._conversionService.Converstion(input, converstionType, conversionUnits));
        }
    }
}
