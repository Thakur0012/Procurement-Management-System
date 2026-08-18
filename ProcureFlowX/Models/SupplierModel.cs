namespace ProcureFlowX.Models
{
    public class SupplierModel
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string SupplierAddress { get; set; }
        public bool IsActive { get; set; }
    }
}