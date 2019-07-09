using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_PivotGrid_CustomFieldSort_Example
{
    public class MyOrderRow
    {
        public int ID { get; internal set; }
        public DateTime OrderDate { get; internal set; }
        public int Quantity { get; internal set; }
        public decimal UnitPrice { get; internal set; }
        public string CustomerName { get; internal set; }
        public string ProductName { get; internal set; }
        public string CategoryName { get; internal set; }
        public string SalesPersonName { get; internal set; }
        public int SalesPersonId { get; internal set; }
        public decimal ExtendedPrice { get; internal set; }
    }
}
