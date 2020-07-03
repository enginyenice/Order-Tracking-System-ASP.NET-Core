using Microsoft.EntityFrameworkCore;
using SiparisTakip.Models.Tables;

namespace SiparisTakip.Models
{
    public class SiparisTakipDB : DbContext
    {
        public SiparisTakipDB(DbContextOptions<SiparisTakipDB> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}