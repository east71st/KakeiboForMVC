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

        public DbSet<KAKEIBO> KAKEIBO { get; set; } = default!;
        public DbSet<HIMOKU> HIMOKU { get; set; } = default!;
    }
}
