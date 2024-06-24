using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MomPosApi.Models
{
    public class MomPosContext:DbContext
    {
        public MomPosContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Menu> Menu { get; set;}=null!;

    }
}
