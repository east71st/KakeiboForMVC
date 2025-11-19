using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using KakeiboForMVC.Data;

namespace KakeiboForMVC.Models
{
    public class KakeiboSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new KakeiboForMVCContext(serviceProvider.
                GetRequiredService<DbContextOptions<KakeiboForMVCContext>>()))
            {
                // Look for any KAKEIBO.
                if (context.KAKEIBO.Any())
                {
                    return;   // DB has been seeded
                }

                context.KAKEIBO.AddRange(
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-06-01"),
                        HIMOKU_ID = 1,
                        MEISAI = "給与",
                        NYUKINGAKU = 200000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-06-01"),
                        HIMOKU_ID = 1,
                        MEISAI = "バイト代",
                        NYUKINGAKU = 20000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-06-02"),
                        HIMOKU_ID = 2,
                        MEISAI = "家賃",
                        SHUKINGAKU = 65000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-06-03"),
                        HIMOKU_ID = 3,
                        MEISAI = "電気",
                        SHUKINGAKU = 15000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-06-03"),
                        HIMOKU_ID = 4,
                        MEISAI = "水道",
                        SHUKINGAKU = 8000,
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-06-03"),
                        HIMOKU_ID = 5,
                        MEISAI = "ガス",
                        SHUKINGAKU = 15000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-06-04"),
                        HIMOKU_ID = 6,
                        MEISAI = "生命保険",
                        SHUKINGAKU = 50000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-06-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "米",
                        SHUKINGAKU = 5000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-06-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "納豆",
                        SHUKINGAKU = 100
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-06-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "牛乳",
                        SHUKINGAKU = 200
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-06-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "卵",
                        SHUKINGAKU = 200
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-06-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "コーヒー",
                        SHUKINGAKU = 200
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-06-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "コーラ",
                        SHUKINGAKU = 200
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-06-05"),
                        HIMOKU_ID = 8,
                        MEISAI = "ティッシュ",
                        SHUKINGAKU = 250
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-06-05"),
                        HIMOKU_ID = 8,
                        MEISAI = "洗剤",
                        SHUKINGAKU = 350
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-06-05"),
                        HIMOKU_ID = 8,
                        MEISAI = "トイレットペーパー",
                        SHUKINGAKU = 350
                    },

                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-07-01"),
                        HIMOKU_ID = 1,
                        MEISAI = "給与",
                        NYUKINGAKU = 200000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-07-01"),
                        HIMOKU_ID = 1,
                        MEISAI = "バイト代",
                        NYUKINGAKU = 20000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-07-02"),
                        HIMOKU_ID = 2,
                        MEISAI = "家賃",
                        SHUKINGAKU = 65000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-07-03"),
                        HIMOKU_ID = 3,
                        MEISAI = "電気",
                        SHUKINGAKU = 15000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-07-03"),
                        HIMOKU_ID = 4,
                        MEISAI = "水道",
                        SHUKINGAKU = 8000,
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-07-03"),
                        HIMOKU_ID = 5,
                        MEISAI = "ガス",
                        SHUKINGAKU = 15000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-07-04"),
                        HIMOKU_ID = 6,
                        MEISAI = "生命保険",
                        SHUKINGAKU = 50000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-07-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "米",
                        SHUKINGAKU = 5000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-07-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "納豆",
                        SHUKINGAKU = 100
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-07-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "牛乳",
                        SHUKINGAKU = 200
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-07-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "卵",
                        SHUKINGAKU = 200
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-07-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "コーヒー",
                        SHUKINGAKU = 250
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-07-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "コーラ",
                        SHUKINGAKU = 150
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-07-05"),
                        HIMOKU_ID = 8,
                        MEISAI = "ティッシュ",
                        SHUKINGAKU = 250
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-07-05"),
                        HIMOKU_ID = 8,
                        MEISAI = "洗剤",
                        SHUKINGAKU = 350
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-07-05"),
                        HIMOKU_ID = 8,
                        MEISAI = "トイレットペーパー",
                        SHUKINGAKU = 350
                    },

                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-08-01"),
                        HIMOKU_ID = 1,
                        MEISAI = "給与",
                        NYUKINGAKU = 200000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-08-01"),
                        HIMOKU_ID = 1,
                        MEISAI = "バイト代",
                        NYUKINGAKU = 20000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-08-02"),
                        HIMOKU_ID = 2,
                        MEISAI = "家賃",
                        SHUKINGAKU = 65000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-08-03"),
                        HIMOKU_ID = 3,
                        MEISAI = "電気",
                        SHUKINGAKU = 15000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-08-03"),
                        HIMOKU_ID = 4,
                        MEISAI = "水道",
                        SHUKINGAKU = 8000,
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-08-03"),
                        HIMOKU_ID = 5,
                        MEISAI = "ガス",
                        SHUKINGAKU = 15000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-08-04"),
                        HIMOKU_ID = 6,
                        MEISAI = "生命保険",
                        SHUKINGAKU = 50000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-08-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "米",
                        SHUKINGAKU = 5000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-08-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "納豆",
                        SHUKINGAKU = 100
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-08-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "牛乳",
                        SHUKINGAKU = 200
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-08-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "卵",
                        SHUKINGAKU = 200
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-08-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "コーヒー",
                        SHUKINGAKU = 250
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-08-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "コーラ",
                        SHUKINGAKU = 150
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-08-05"),
                        HIMOKU_ID = 8,
                        MEISAI = "ティッシュ",
                        SHUKINGAKU = 250
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-08-05"),
                        HIMOKU_ID = 8,
                        MEISAI = "洗剤",
                        SHUKINGAKU = 350
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-08-05"),
                        HIMOKU_ID = 8,
                        MEISAI = "トイレットペーパー",
                        SHUKINGAKU = 350
                    },

                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-09-01"),
                        HIMOKU_ID = 1,
                        MEISAI = "給与",
                        NYUKINGAKU = 200000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-09-01"),
                        HIMOKU_ID = 1,
                        MEISAI = "バイト代",
                        NYUKINGAKU = 20000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-09-02"),
                        HIMOKU_ID = 2,
                        MEISAI = "家賃",
                        SHUKINGAKU = 65000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-09-03"),
                        HIMOKU_ID = 3,
                        MEISAI = "電気",
                        SHUKINGAKU = 15000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-09-03"),
                        HIMOKU_ID = 4,
                        MEISAI = "水道",
                        SHUKINGAKU = 8000,
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-09-03"),
                        HIMOKU_ID = 5,
                        MEISAI = "ガス",
                        SHUKINGAKU = 15000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-09-04"),
                        HIMOKU_ID = 6,
                        MEISAI = "生命保険",
                        SHUKINGAKU = 50000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-09-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "米",
                        SHUKINGAKU = 5000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-09-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "納豆",
                        SHUKINGAKU = 100
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-09-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "牛乳",
                        SHUKINGAKU = 200
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-09-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "卵",
                        SHUKINGAKU = 200
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-09-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "コーヒー",
                        SHUKINGAKU = 250
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-09-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "コーラ",
                        SHUKINGAKU = 150
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-09-05"),
                        HIMOKU_ID = 8,
                        MEISAI = "ティッシュ",
                        SHUKINGAKU = 250
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-09-05"),
                        HIMOKU_ID = 8,
                        MEISAI = "洗剤",
                        SHUKINGAKU = 350
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-09-05"),
                        HIMOKU_ID = 8,
                        MEISAI = "トイレットペーパー",
                        SHUKINGAKU = 350
                    },

                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-10-01"),
                        HIMOKU_ID = 1,
                        MEISAI = "給与",
                        NYUKINGAKU = 200000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-10-01"),
                        HIMOKU_ID = 1,
                        MEISAI = "バイト代",
                        NYUKINGAKU = 20000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-10-02"),
                        HIMOKU_ID = 2,
                        MEISAI = "家賃",
                        SHUKINGAKU = 65000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-10-03"),
                        HIMOKU_ID = 3,
                        MEISAI = "電気",
                        SHUKINGAKU = 15000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-10-03"),
                        HIMOKU_ID = 4,
                        MEISAI = "水道",
                        SHUKINGAKU = 8000,
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-10-03"),
                        HIMOKU_ID = 5,
                        MEISAI = "ガス",
                        SHUKINGAKU = 15000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-10-04"),
                        HIMOKU_ID = 6,
                        MEISAI = "生命保険",
                        SHUKINGAKU = 50000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-10-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "米",
                        SHUKINGAKU = 5000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-10-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "納豆",
                        SHUKINGAKU = 100
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-10-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "牛乳",
                        SHUKINGAKU = 200
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-10-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "卵",
                        SHUKINGAKU = 200
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-10-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "コーヒー",
                        SHUKINGAKU = 250
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-10-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "コーラ",
                        SHUKINGAKU = 150
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-10-05"),
                        HIMOKU_ID = 8,
                        MEISAI = "ティッシュ",
                        SHUKINGAKU = 250
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-10-05"),
                        HIMOKU_ID = 8,
                        MEISAI = "洗剤",
                        SHUKINGAKU = 350
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-10-05"),
                        HIMOKU_ID = 8,
                        MEISAI = "トイレットペーパー",
                        SHUKINGAKU = 350
                    },

                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-11-01"),
                        HIMOKU_ID = 1,
                        MEISAI = "給与",
                        NYUKINGAKU = 200000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-11-01"),
                        HIMOKU_ID = 1,
                        MEISAI = "バイト代",
                        NYUKINGAKU = 20000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-11-02"),
                        HIMOKU_ID = 2,
                        MEISAI = "家賃",
                        SHUKINGAKU = 65000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-11-03"),
                        HIMOKU_ID = 3,
                        MEISAI = "電気",
                        SHUKINGAKU = 15000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-11-03"),
                        HIMOKU_ID = 4,
                        MEISAI = "水道",
                        SHUKINGAKU = 8000,
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-11-03"),
                        HIMOKU_ID = 5,
                        MEISAI = "ガス",
                        SHUKINGAKU = 15000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-11-04"),
                        HIMOKU_ID = 6,
                        MEISAI = "生命保険",
                        SHUKINGAKU = 50000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-11-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "米",
                        SHUKINGAKU = 5000
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-11-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "納豆",
                        SHUKINGAKU = 100
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-11-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "牛乳",
                        SHUKINGAKU = 200
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-11-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "卵",
                        SHUKINGAKU = 200
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-11-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "コーヒー",
                        SHUKINGAKU = 250
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-11-05"),
                        HIMOKU_ID = 7,
                        MEISAI = "コーラ",
                        SHUKINGAKU = 150
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-11-05"),
                        HIMOKU_ID = 8,
                        MEISAI = "ティッシュ",
                        SHUKINGAKU = 250
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-11-05"),
                        HIMOKU_ID = 8,
                        MEISAI = "洗剤",
                        SHUKINGAKU = 350
                    },
                    new KAKEIBO
                    {
                        ID = 0,
                        HIDUKE = DateTime.Parse("2025-11-05"),
                        HIMOKU_ID = 8,
                        MEISAI = "トイレットペーパー",
                        SHUKINGAKU = 350
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
