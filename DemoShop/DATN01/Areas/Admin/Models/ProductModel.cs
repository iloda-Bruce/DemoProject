using static Azure.Core.HttpHeader;

namespace DATN01.Areas.Admin.Models
{
    public class ProductModel
    {
     //       PID int identity(1,1) primary key,
     //   PGroup nvarchar(50) null,
	    //PClass nvarchar(50) null,
	    //PMark nvarchar(50) null,
	    //PCode nvarchar(50) null,
	    //PName nvarchar(50) null,
	    //PLimit int null,
	    //PCapital float null,
	    //PSell float null,
	    //PWeight float null,
	    //Notes nvarchar(200) null,
	    //isSell bit null,
     //   isActive bit null
		public int PID { set; get; }
        public string? PGroup { set; get; }
        public string? PClass { set; get; }
        public string? PCode { set; get; }
        public string? PName { set; get; }
        public int PLimit { set; get; }
        public decimal PCapital { set; get; } 
        public decimal PSell { set; get; }
        public decimal PWeight { set; get; }
        public string? Notes { set; get; }
        public bool isSell { set; get; }
        public bool isActive { set; get; }
    }
}
