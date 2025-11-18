using KakeiboForMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace KakeiboForMVC.Models
{
    public class HimokuSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new KakeiboForMVCContext(serviceProvider.
                GetRequiredService<DbContextOptions<KakeiboForMVCContext>>()))
            {
                // Look for any HIMOKU.
                if (context.HIMOKU.Any())
                {
                    return;   // DB has been seeded
                }

                context.HIMOKU.AddRange(
                    new HIMOKU
                    {
                        ID = 0,
                        NAME = "収入",
                    },
                    new HIMOKU
                    {
                        ID = 0,
                        NAME = "居住費",
                    },
                    new HIMOKU
                    {
                        ID = 0,
                        NAME = "電気代",
                    },
                    new HIMOKU
                    {
                        ID = 0,
                        NAME = "水道代",
                    },
                    new HIMOKU
                    {
                        ID = 0,
                        NAME = "ガス代",
                    },
                    new HIMOKU
                    {
                        ID = 0,
                        NAME = "保険代",
                    },
                    new HIMOKU
                    {
                        ID = 0,
                        NAME = "食費",
                    },
                    new HIMOKU
                    {
                        ID = 0,
                        NAME = "日用品費",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
