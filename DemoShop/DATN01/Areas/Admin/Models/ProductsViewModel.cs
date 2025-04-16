namespace DATN01.Areas.Admin.Models
{
    public class ProductsViewModel
    {
        public int PID { set; get; }
        public string? PGroup { set; get; }
        public string? PClass { set; get; }
        public string? PCode { set; get; }
        public string? PName { set; get; }
        public string? PMark { set; get; }
        public int PLimit { set; get; }
        public decimal PCapital { set; get; }
        public decimal PSell { set; get; }
        public decimal PWeight { set; get; }
        public string? Notes { set; get; }
        public bool isSell { set; get; }
        public bool isActive { set; get; }
        //public ProductModel? Product { set; get; }
        public int PReserves { set; get; }
    }
}
