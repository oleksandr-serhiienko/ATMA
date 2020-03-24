using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ATMA.Models;
using ATMA.Repository;

namespace ATMA.Controllers
{
    [Produces("application/json")]
    [Route("api/items")]
    [ApiController]
    public class SaleDatasController : ControllerBase
    {
        private ISaleDataRepository saleDataRepository;

        public SaleDatasController(SaleDataContext context)
        {
            saleDataRepository = new SaleDataRepository(context);
        }

        /// <summary>
        /// Gets all the sale data
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET/ https://localhost:5001/api/items
        ///     GET/ https://localhost:5001/api/items?date=2020-03-24
        ///
        /// </remarks>
        /// <param name="saleDataParameter"> use ?date= to specify the date in format of yyy-mmm-dd </param>
        /// <response code="200">Returns the requested list of Sale Data</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SaleData>>> GetSaleDatas([FromQuery]SaleDataParameter saleDataParameter)
        {       
            if (saleDataParameter.HasDate)
                return await saleDataRepository.GetSaleDatas(saleDataParameter.Date ?? DateTime.Now);
            else
                return await saleDataRepository.GetSaleDatas();
        }

        /// <summary>
        /// Gets the number of the posted sale data for the specific time.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET/ https://localhost:5001/api/items/number
        ///     GET/ https://localhost:5001/api/items/number?date=2018-11-11
        ///     
        /// </remarks>
        /// <returns>
        /// number of the sold items by time period:
        ///                                         4              
        /// </returns>
        /// <response code="200">Returns the requested  Sale Data</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("number")]
        public ActionResult<int> GetNumberOfSoldArticles([FromQuery]SaleDataParameter saleDataParameter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (saleDataParameter.HasDate)
                return saleDataRepository.GetNumberOfSoldArticles(saleDataParameter.Date ?? DateTime.Now);
            else
                return saleDataRepository.GetNumberOfSoldArticles();
        }

        /// <summary>
        /// Gets the revenue of the specific period.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET/ https://localhost:5001/api/items/revenue
        ///     GET/ https://localhost:5001/api/items/revenue?date=2018-11-11
        ///
        /// </remarks>
        /// <returns>
        /// revenue of the sold items by time period:
        ///                                         4353 (in euro)              
        /// </returns>
        /// <response code="200">Returns the requested Sale Data</response>
        [HttpGet("revenue")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<double> GetRevenue([FromQuery]SaleDataParameter saleDataParameter)
        {

            if (saleDataParameter.HasDate)
                return saleDataRepository.GetRevenue(saleDataParameter.Date ?? DateTime.Now);
            else
                return saleDataRepository.GetRevenue();
        }

        /// <summary>
        /// Gets all the posted sale data grouped by the article and with summed price
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET/ https://localhost:5001/api/items/revenue-by-article?date=2018-11-11(and)name=beer
        ///     GET/ https://localhost:5001/api/items/revenue-by-article?date=2018-11-11
        ///     GET/ https://localhost:5001/api/items/revenue-by-article?name=beer
        ///     GET/ https://localhost:5001/api/items/revenue-by-article
        ///
        /// </remarks>
        /// <returns>
        /// Requested data depending on the request. When the request specifies the date, all the articles will be grouped and returned.
        ///                                          When the request specifies the date and the article, only specified article revenue of the specified day will be returnde.
        ///                                          When the request specifies the article, only specified article revenue of all the time will be returned.
        ///                                          When the request does not specify anything, all the data grouped by articles for all the time will be returned.
        ///[
        ///    {
        ///        "articleNumber": "beer",
        ///        "price": 36
        ///    },
        ///    {
        ///        "articleNumber": "water",
        ///        "price": 18
        ///    },
        ///    {
        ///        "articleNumber": "cat",
        ///        "price": 364
        ///    }
        ///]
        ///</returns>
        /// <response code="200">Returns the requested list of Sale Data</response>
        [HttpGet("revenue-by-article")]
        public IEnumerable<SaleDataDTO> GetRevenueByArticles([FromQuery]SaleDataParameter saleDataParameter)
        {
            if (saleDataParameter.HasDate && saleDataParameter.HasName)
                return new[] { saleDataRepository.GetRevenueByArticles(saleDataParameter.Name, saleDataParameter.Date ?? DateTime.Now) };
            else if (saleDataParameter.HasDate)
                return saleDataRepository.GetRevenueByArticles(saleDataParameter.Date ?? DateTime.Now);
            else if (saleDataParameter.HasName)
                return new[] { saleDataRepository.GetRevenueByArticles(saleDataParameter.Name) };
            else return saleDataRepository.GetRevenueByArticles();

        }

        /// <summary>
        /// Gets the requested sale data.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET/ https://localhost:5001/api/items/1
        ///     
        /// </remarks>
        /// <param name="id"> index of the data </param>
        /// <returns>
        /// Sale data by id
        /// </returns>
        /// <response code="200">Returns the requested  Sale Data</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<SaleData>> GetSaleData(int id)
        {
            var saleData = await saleDataRepository.FindSaleData(id);

            if (saleData == null)
            {
                return NotFound();
            }

            return saleData;
        }

        /// <summary>
        /// Creates a SaleData.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / https://localhost:5001/api/items
        ///     {
        ///        "id": 1,
        ///        "name": "Cat",
        ///        "Price": 33
        ///     }
        ///
        /// </remarks>
        /// <param name="saleDataDTO"></param>
        /// <returns>A newly created Sale Data</returns>
        /// <response code="201">Returns the newly created Sale Data</response>
        /// <response code="400">If the item is null</response>            
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<SaleData>> PostSaleData(SaleDataDTO saleDataDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SaleData saleData = await saleDataRepository.PostSaleData(saleDataDTO);

            return CreatedAtAction(nameof(GetSaleData), new { id = saleData.ID }, saleData);
        }

        /// <summary>
        /// Deletes the requested sale data.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete/ https://localhost:5001/api/items/1
        ///     
        /// </remarks>
        /// <param name="id"> index of the data </param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<SaleData>> DeleteSaleData(int id)
        {
            var saleData = await saleDataRepository.FindSaleData(id);
            if (saleData == null)
            {
                return NotFound();
            }

            await saleDataRepository.DeleteSaleData(saleData);

            return saleData;
        }

    }
}
