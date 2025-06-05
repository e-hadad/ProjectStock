
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Services;

namespace Stock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly AlphaVantageService _alphaVantageService;

        public StockController(AlphaVantageService alphaVantageService)
        {
            _alphaVantageService = alphaVantageService;
        }

        [HttpGet("day")]
        public async Task<ActionResult<TwelveDataValue>> GetDay(string s)
        {
            var stockData = await _alphaVantageService.GetStockDataAsync(s);
            var latest = stockData.Values.OrderByDescending(v => v.DateTime).FirstOrDefault();
            if (latest == null) return NotFound("No data found");

            return Ok(latest);
        }

        [HttpGet("week")]
        public async Task<ActionResult<IEnumerable<TwelveDataValue>>> GetWeek(string s)
        {
            var stockData = await _alphaVantageService.GetStockDataAsync(s);
            var weekData = stockData.Values.OrderByDescending(v => v.DateTime).Take(5);
            return Ok(weekData);
        }

        [HttpGet("month")]
        public async Task<ActionResult<IEnumerable<TwelveDataValue>>> GetMonth(string s)
        {
            var stockData = await _alphaVantageService.GetStockDataAsync(s);
            var monthData = stockData.Values.OrderByDescending(v => v.DateTime).Take(22);
            return Ok(monthData);
        }

        [HttpGet("3month")]
        public async Task<ActionResult<IEnumerable<TwelveDataValue>>> Get3Month(string s)
        {
            var stockData = await _alphaVantageService.GetStockDataAsync(s);
            var threeMonthData = stockData.Values.OrderByDescending(v => v.DateTime).Take(63);
            return Ok(threeMonthData);
        }
        [HttpGet("halfyear")]
        public async Task<ActionResult<IEnumerable<TwelveDataValue>>> GetHalfYear(string s)
        {
            var stockData = await _alphaVantageService.GetStockDataAsync(s);
            var halfYearData = stockData.Values
                .OrderByDescending(v => v.DateTime)
                .Take(126); // בערך חצי שנה של ימי מסחר (21 ימים כפול 6 חודשים)

            return Ok(halfYearData);
        }

        [HttpGet("year")]
        public async Task<ActionResult<IEnumerable<TwelveDataValue>>> GetYear(string s)
        {
            var stockData = await _alphaVantageService.GetStockDataAsync(s);
            var yearData = stockData.Values.OrderByDescending(v => v.DateTime).Take(252);
            return Ok(yearData);
        }
    }
}
