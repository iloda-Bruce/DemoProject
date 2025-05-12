using DATN01.Models;

namespace DATN01.Areas.Admin.Models
{
    public class CustomerAccountModel
    {
        public int CID {  get; set; }
        public string CName {  get; set; }
        public string CAddress {  get; set; }
        public string CPhone {  get; set; }
        public DateTime CDOB { get; set; }
        public int AID { get; set; }
        public string AName {  get; set; }
        public string APass {  get; set; }
        public int EID {  get; set; }
        public string ARole {  get; set; }
    }
}
