using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ATMA.Models
{
    public class SaleData : IValidatableObject
    {

        public int ID { get; set; }

        [Required]
        [StringLength(32)]
        public string ArticleNumber { get; set; }

        public double Price { get; set; }

        public DateTime CreateDate { get; set; }

        static public SaleData ConvertFromDTO(SaleDataDTO saleDataDTO)
        {
            SaleData saleData = new SaleData();
            saleData.ArticleNumber = saleDataDTO.ArticleNumber;
            saleData.Price = saleDataDTO.Price;
            saleData.CreateDate = DateTime.Now;
            return saleData;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ArticleNumber.Length >= 32)
            {
                yield return new ValidationResult("The name should be not more then 32 symbols");
            }         
        }
    }
}
