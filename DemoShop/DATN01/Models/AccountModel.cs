using System.ComponentModel.DataAnnotations;

namespace DATN01.Models
{
    public class AccountModel
    {
        //       AID int identity(1,1) primary key,
        //   AName nvarchar(50) null,
        //APass nvarchar(50) null,
        //EID int null,
        //CID int null
        //[Key]
        public int AID { get; set; }
        public string? AName { get; set; }
        public string? APass { get; set; }
        public int? EID { get; set; }
        public int? CID { get; set; }
        public string ? ARole { set; get; }
    }
}
