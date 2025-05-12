using Microsoft.AspNetCore.Mvc;

namespace DATN01.Areas.Repository
{
    public interface IProductServices
    {
        public IActionResult GetProducts();
    }
}
