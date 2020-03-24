using ATMA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATMA.Repository
{
    public class SaleDataRepository : ISaleDataRepository
    {
        private readonly SaleDataContext _context;

        public SaleDataRepository(SaleDataContext context)
        {
            _context = context;
        }

        public int GetNumberOfSoldArticles(DateTime day)
        {
            return  _context.SaleDatas.Where(sd => sd.CreateDate.Date == day.Date).ToListAsync().Result.Count;
        }

        public int GetNumberOfSoldArticles()
        {
            return _context.SaleDatas.ToListAsync().Result.Count;
        }

        public async Task<ActionResult<IEnumerable<SaleData>>> GetSaleDatas()
        {
            return await _context.SaleDatas.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<SaleData>>> GetSaleDatas(DateTime day)
        {
            return await _context.SaleDatas.Where(sd => sd.CreateDate.Date == day.Date).ToListAsync();
        }

        public double GetRevenue(DateTime day)
        {
            return _context.SaleDatas.Where(sd => sd.CreateDate.Date == day.Date).ToListAsync().Result.Sum(sd => sd.Price);
        }

        public double GetRevenue()
        {
            return _context.SaleDatas.ToListAsync().Result.Sum(sd => sd.Price);
        }

        public IEnumerable<SaleDataDTO> GetRevenueByArticles()
        {
            return _context.SaleDatas.ToListAsync().Result
                                     .GroupBy(sd => sd.ArticleNumber)
                                            .Select(g => new SaleDataDTO
                                            { ArticleNumber = g.Key, Price = g.Sum(r => r.Price) });
        }

        public SaleDataDTO GetRevenueByArticles(string articleNumber)
        {
            var sum = _context.SaleDatas.Where(tr => tr.ArticleNumber == articleNumber)
                                            .ToListAsync().Result.Sum(tr => tr.Price);

            return new SaleDataDTO { ArticleNumber = articleNumber, Price = sum };
                                            
        }

        public SaleDataDTO GetRevenueByArticles(string articleNumber, DateTime day)
        {
            var sum = _context.SaleDatas.Where(tr => tr.ArticleNumber == articleNumber && tr.CreateDate.Date == day.Date)
                                            .ToListAsync().Result.Sum(tr => tr.Price);

            return new SaleDataDTO { ArticleNumber = articleNumber, Price = sum };


        }

        public IEnumerable<SaleDataDTO> GetRevenueByArticles(DateTime day)
        {
            return _context.SaleDatas.Where(sd => sd.CreateDate.Date.Date == day.Date)
                                     .ToListAsync().Result
                                     .GroupBy(sd => sd.ArticleNumber)
                                            .Select(g => new SaleDataDTO
                                            { ArticleNumber = g.Key, Price = g.Sum(r => r.Price) });
        }

        public async Task<SaleData> FindSaleData(int id)
        {
            return await _context.SaleDatas.FindAsync(id);
        }

        public async Task<SaleData> PostSaleData(SaleDataDTO saleDataDTO)
        {
            SaleData saleData = SaleData.ConvertFromDTO(saleDataDTO);
            _context.SaleDatas.Add(saleData);
            await _context.SaveChangesAsync();

            return saleData;
        }

        public async Task DeleteSaleData(SaleData saleData)
        {
            _context.SaleDatas.Remove(saleData);
            await _context.SaveChangesAsync();
        }
    }
}
