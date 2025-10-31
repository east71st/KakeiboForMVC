using KakeiboForMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace KakeiboForMVC.Models
{
    public class HimokuSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new KakeiboForMVCContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<KakeiboForMVCContext>>()))
            {
                // Look for any HIMOKU.
                if (context.HIMOKU.Any())
                {
                    return;   // DB has been seeded
                }

                context.HIMOKU.AddRange(
                    new HIMOKU
                    {
                        ID=1,
                        NAME = "収入",
                    },
                    new HIMOKU
                    {
                        ID=2,
                        NAME = "居住費",
                    },
                    new HIMOKU
                    {
                        ID=3,
                        NAME = "電気代",
                    }, 
                    new HIMOKU
                    {
                        ID=4,
                        NAME = "水道代",
                    }, 
                    new HIMOKU
                    {
                        ID=5,
                        NAME = "ガス代",
                    }, 
                    new HIMOKU
                    {
                        ID=6,
                        NAME = "保険代",
                    }, 
                    new HIMOKU
                    {
                        ID=7,
                        NAME = "食費",
                    }, 
                    new HIMOKU
                    {
                        ID=8,
                        NAME = "日用品費",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
