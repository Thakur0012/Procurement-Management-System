using System;
using System.Collections.Generic;

namespace ProcureFlowX.Models
{
    public class GoodsReceiptModel
    {
        public GoodsReceiptModel()
        {
            // Initialize Items list to prevent NullReferenceException
            Items = new List<GoodsReceiptItemModel>();
        }

        public int GRNId { get; set; }
        public int SupplierId { get; set; }
        public DateTime GRNDate { get; set; }
        public string GRNStatus { get; set; }

        public string SupplierName { get; set; }
        public string ContactNumber { get; set; }
        public string SupplierAddress { get; set; }

        
        public List<GoodsReceiptItemModel> Items { get; set; }
    }

    public class GoodsReceiptItemModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int StockQty { get; set; }
        public int ReceivedQty { get; set; }
        public decimal UnitPrice { get; set; }   
        public decimal UnitRate { get; set; }
        public decimal LineTotal { get; set; }
    }
}
