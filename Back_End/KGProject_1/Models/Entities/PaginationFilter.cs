using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KGProject_1.Models.Entities
{
    public class PaginationFilter
    {
        private int pageNumber;
        private int pageSize;
        private string sortstring;
        public PaginationFilter()
        {

        }
        public PaginationFilter(int a, int b, string c)
        {
            PageNumber = a;
            PageSize = b;
            Sortstring = c;
        }

        public int PageSize { get => pageSize; set => pageSize = value; }
        public int PageNumber { get => pageNumber; set => pageNumber = value; }
        public string Sortstring { get => sortstring; set => sortstring = value; }
    }
}
