using Microsoft.EntityFrameworkCore;
using SiparisTakip.Models.Tables;
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
        public DbSet<Request> Requests { get; set; }
    }
}
