using Microsoft.EntityFrameworkCore;


namespace Minenetred.web.Context
{
    public class MinenetredContext : DbContext
    {
        //public DbSet<Contact> Contacts { get; set; }

        public MinenetredContext(DbContextOptions<MinenetredContext> options)
            : base(options)
        {

        }
    }
}
