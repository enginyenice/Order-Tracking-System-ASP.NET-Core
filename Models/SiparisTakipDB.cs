using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiparisTakip.Models
{
    public class SiparisTakipDB : DbContext
    {
        public SiparisTakipDB(DbContextOptions<SiparisTakipDB> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
