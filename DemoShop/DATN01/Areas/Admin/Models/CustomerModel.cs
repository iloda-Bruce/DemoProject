namespace DATN01.Areas.Admin.Models
{
    public class CustomerModel
    {
      //  [CID]
      //,[CName]
      //,[CAddress]
      //,[CPhone]
      //,[CDOB]
        public int CID { get; set; }
        public string CName { get; set; }
        public string CAddress { get; set; }
        public string CPhone { get; set; }
        public DateTime CDOB { get; set; }
    }
}
