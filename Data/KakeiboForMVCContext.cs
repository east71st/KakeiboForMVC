using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KakeiboForMVC.Models;

namespace KakeiboForMVC.Data
{
    public class KakeiboForMVCContext : DbContext
    {
        public KakeiboForMVCContext (DbContextOptions<KakeiboForMVCContext> options)
            : base(options)
        {
        }

        public DbSet<KakeiboForMVC.Models.KAKEIBO> KAKEIBO { get; set; } = default!;
        public DbSet<KakeiboForMVC.Models.HIMOKU> HIMOKU { get; set; } = default!;
    }
}
