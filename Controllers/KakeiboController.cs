using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KakeiboForMVC.Data;
using KakeiboForMVC.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace KakeiboForMVC.Controllers
{
    public class KakeiboController : Controller
    {
        private readonly KakeiboForMVCContext _context;

        public KakeiboController(KakeiboForMVCContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Input()
        {
            InputViewModel viewModel = new ()
            {
                InputDate =  DateTime.Today,
                HimokuNameSelect = await _context.HIMOKU.Select(x => new SelectListItem(x.NAME, x.ID.ToString())).ToListAsync(),
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Input(DateTime inputDate, int himokuId, string? meisai, decimal? kingaku)
        {
            InputViewModel viewModel = new()
            {
                InputDate = inputDate,
                HimokuId = himokuId,
                Meisai = meisai,
                Kingaku = kingaku,
                HimokuNameSelect = await _context.HIMOKU.Select(x => new SelectListItem(x.NAME, x.ID.ToString())).ToListAsync(),
            };

            if (!ModelState.IsValid)
            {

            }
            else
            {
                KAKEIBO kakeibo = new KAKEIBO()
                {
                    HIDUKE = inputDate,
                    HIMOKU_ID = himokuId,
                    MEISAI = meisai,
                    NYUKINGAKU = himokuId == 1 ? kingaku : 0,
                    SHUKINGAKU = himokuId != 1 ? kingaku : 0,
                };

                _context.Add(kakeibo);

                _context.SaveChanges();
            }

            viewModel.KakeiboList = await _context.KAKEIBO.Select(x => new KakeiboRecord
            {
                Id = x.ID,
                Hiduke = x.HIDUKE,
                HimokuId = x.HIMOKU_ID,
                Meisai = x.MEISAI,
                NyukinGaku = x.NYUKINGAKU,
                ShukinGaku = x.SHUKINGAKU,
            }).ToListAsync();

            return View(viewModel);
        }

        /// <summary>
        /// 表示画面
        /// </summary>
        /// <param name="firstDate"></param>検索開始期間
        /// <param name="lastDate"></param>検索終了期間
        /// <param name="himokuId"></param>検索費目ID
        /// <param name="meisai"></param>検索明細
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Display(DateTime? firstDate, DateTime? lastDate, int? himokuId, string? meisai)
        {
            firstDate = firstDate ?? new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            lastDate = lastDate ?? new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));

            var query = _context.KAKEIBO.Select(x => new KakeiboRecord()
            {
                Id = x.ID,
                Hiduke = x.HIDUKE,
                HimokuId = x.HIMOKU_ID,
                HimokuName = _context.HIMOKU.FirstOrDefault(y => y.ID == x.HIMOKU_ID) == null ? null : _context.HIMOKU.FirstOrDefault(y => y.ID == x.HIMOKU_ID)!.NAME,
                Meisai = x.MEISAI,
                NyukinGaku = x.NYUKINGAKU,
                ShukinGaku = x.SHUKINGAKU,
            });

            if (firstDate != null)
            {
                query = query.Where(x => x.Hiduke >= firstDate);
            }

            if (lastDate != null)
            {
                query = query.Where(x => x.Hiduke <= lastDate);
            }

            if (himokuId != null && himokuId != 0)
            {
                query = query.Where(x => x.HimokuId == himokuId);
            }

            if (!string.IsNullOrWhiteSpace(meisai))
            {
                query = query.Where(x => !string.IsNullOrEmpty(x.Meisai) && x.Meisai.Contains(meisai));
            }

            DisplayViewModel viewModel = new()
            {
                FirstDate = firstDate,
                LastDate = lastDate,
                HimokuId = himokuId,
                Meisai = meisai,
                KakeiboList = await query.ToListAsync(),
                HimokuNameSelect = await _context.HIMOKU.Select(x => new SelectListItem(x.NAME, x.ID.ToString())).ToListAsync(),
            };

            return View(viewModel);
        }











        // GET: Kakeibo
        public async Task<IActionResult> Index()
        {
            return View(await _context.KAKEIBO.ToListAsync());
        }

        // GET: Kakeibo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kAKEIBO = await _context.KAKEIBO
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kAKEIBO == null)
            {
                return NotFound();
            }

            return View(kAKEIBO);
        }

        // GET: Kakeibo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kakeibo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,HIDUKE,HIMOKU_ID,MEISAI,NYUKINGAKU,SHUKINGAKU")] KAKEIBO kAKEIBO)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kAKEIBO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kAKEIBO);
        }

        // GET: Kakeibo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kAKEIBO = await _context.KAKEIBO.FindAsync(id);
            if (kAKEIBO == null)
            {
                return NotFound();
            }
            return View(kAKEIBO);
        }

        // POST: Kakeibo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,HIDUKE,HIMOKU_ID,MEISAI,NYUKINGAKU,SHUKINGAKU")] KAKEIBO kAKEIBO)
        {
            if (id != kAKEIBO.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kAKEIBO);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KAKEIBOExists(kAKEIBO.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(kAKEIBO);
        }

        // GET: Kakeibo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kAKEIBO = await _context.KAKEIBO
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kAKEIBO == null)
            {
                return NotFound();
            }

            return View(kAKEIBO);
        }

        // POST: Kakeibo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kAKEIBO = await _context.KAKEIBO.FindAsync(id);
            if (kAKEIBO != null)
            {
                _context.KAKEIBO.Remove(kAKEIBO);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KAKEIBOExists(int id)
        {
            return _context.KAKEIBO.Any(e => e.ID == id);
        }
    }
}
