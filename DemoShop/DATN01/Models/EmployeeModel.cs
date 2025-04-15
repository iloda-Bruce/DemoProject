namespace DATN01.Models
{
    public class EmployeeModel
    {
      //  [EID]
      //,[EName]
      //,[EAddress]
      //,[EPhone]
      //,[EDOB]

        public int EID { set; get; }
        public string? EName { set; get; }
        public string? EAddress { set; get; }
        public string? EPhone { set; get; }
        public DateTime? EDOB { set; get; }
      
    }
}
