namespace ProcureFlowX.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; }

        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int StockQty { get; set; }
        public bool IsActive { get; set; }
    }
}