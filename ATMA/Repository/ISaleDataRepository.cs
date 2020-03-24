using ATMA.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATMA.Repository
{
    interface ISaleDataRepository
    {
        int GetNumberOfSoldArticles();

        int GetNumberOfSoldArticles(DateTime day);

        Task<ActionResult<IEnumerable<SaleData>>> GetSaleDatas();

        Task<ActionResult<IEnumerable<SaleData>>> GetSaleDatas(DateTime day);        

        double GetRevenue();

        double GetRevenue(DateTime day);

        IEnumerable<SaleDataDTO> GetRevenueByArticles();

        IEnumerable<SaleDataDTO> GetRevenueByArticles(DateTime day);

        Task<SaleData> FindSaleData(int id);

        SaleDataDTO GetRevenueByArticles(string articleNumber);

        SaleDataDTO GetRevenueByArticles(string articleNumber, DateTime day);

        Task<SaleData> PostSaleData(SaleDataDTO saleDataDTO);

        Task DeleteSaleData(SaleData saleData);
    }
}
