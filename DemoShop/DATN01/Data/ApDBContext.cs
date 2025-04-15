using Microsoft.EntityFrameworkCore;

namespace DATN01.Data
{
    public class ApDBContext: DbContext
    {
        protected ApDBContext(DbContextOptions<ApDBContext> options):base(options)
        {
        }

    }
}
