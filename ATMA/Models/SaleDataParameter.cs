using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATMA.Models
{
    public class SaleDataParameter 
    {
        /// <summary>
        /// use ?date= to specify the date in format of yyy-mmm-dd
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// use ?name = article name in revenue-by-articles
        /// </summary>
        public string Name { get; set; }

        public bool HasDate => Date != null;

        public bool HasName => Name != null;
    }
}
