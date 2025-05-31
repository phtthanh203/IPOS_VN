using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPOS.DB
{
    public class ProductDetails
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }

        public string ImagePath { get; set; }

        public int CategoryId { get; set; }

        ForeignKeyConstraint ForeignKeyConstraintCategoryId { get; set; }



    }

}