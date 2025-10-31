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
    /// <summary>
    /// 家計簿コントローラ
    /// </summary>
    /// <param name="context"></param>
    public class KakeiboController(KakeiboForMVCContext context) : Controller
    {
        private readonly KakeiboForMVCContext _context = context;

        /// <summary>
        /// メニュー表示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 表示画面
        /// </summary>
        /// <param name="inputDate"></param>
        /// <param name="himokuId"></param>
        /// <param name="meisai"></param>
        /// <param name="kingaku"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Input(DateTime? inputDate, int? himokuId, string? meisai, decimal? kingaku)
        {
            InputViewModel viewModel = new()
            {
                InputDate = inputDate ?? DateTime.Now,
                HimokuId = himokuId,
                Meisai = meisai,
                Kingaku = kingaku,
                HimokuNameSelect = await _context.HIMOKU.
                    Select(x => new SelectListItem(x.NAME, x.ID.ToString())).
                    ToListAsync(),
            };

            viewModel.KakeiboList = await _context.
                KAKEIBO.OrderByDescending(x => x.ID).
                Select(x => new KakeiboRecord()
                {
                    Id = x.ID,
                    Hiduke = x.HIDUKE,
                    HimokuId = x.HIMOKU_ID,
                    HimokuName = _context.HIMOKU.Where(x => x.ID == himokuId).Select(x => x.NAME).ToString(),
                    Meisai = x.MEISAI,
                    NyukinGaku = x.NYUKINGAKU,
                    ShukinGaku = x.SHUKINGAKU,
                }).
                ToListAsync();

            return View(viewModel);
        }

        /// <summary>
        /// 表示画面
        /// </summary>
        /// <param name="firstDate"><検索開始期間/param>
        /// <param name="lastDate">検索終了期間</param>
        /// <param name="himokuId">検索費目ID</param>
        /// <param name="meisai">検索明細</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Display(DateTime? firstDate, DateTime? lastDate, int? himokuId, string? meisai)
        {
            firstDate ??= new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            lastDate ??= new DateTime(DateTime.Today.Year, DateTime.Today.Month,
                DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));

            var query = _context.KAKEIBO.Select(x => new KakeiboRecord()
            {
                Id = x.ID,
                Hiduke = x.HIDUKE,
                HimokuId = x.HIMOKU_ID,
                HimokuName = _context.HIMOKU.Where(x => x.ID == himokuId).Select(x => x.NAME).ToString(),
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

            query = query.OrderBy(x => x.Hiduke).ThenBy(x => x.HimokuId).ThenBy(x => x.Meisai);

            DisplayViewModel viewModel = new()
            {
                FirstDate = firstDate,
                LastDate = lastDate,
                HimokuId = himokuId,
                HimokuName = _context.HIMOKU.Where(x => x.ID == himokuId).Select(x => x.NAME).ToString(),
                Meisai = meisai,
                KakeiboList = await query.ToListAsync(),
                HimokuNameSelect = await _context.HIMOKU.Select(x => new SelectListItem(x.NAME, x.ID.ToString())).ToListAsync(),
            };

            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Update(DateTime? firstDate, DateTime? lastDate, int? himokuId, string? meisai)
        {
            firstDate ??= new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            lastDate ??= new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));

            var query = _context.KAKEIBO.Select(x => new KakeiboRecord()
            {
                Id = x.ID,
                Hiduke = x.HIDUKE,
                HimokuId = x.HIMOKU_ID,
                HimokuName = _context.HIMOKU.Where(y => y.ID == x.HIMOKU_ID).Select(y => y.NAME).ToString(),
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

            query = query.OrderBy(x => x.Hiduke).ThenBy(x => x.HimokuId).ThenBy(x => x.Meisai);

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

        [HttpPost]
        public IActionResult Updete(UpdateViewModel viewModel)
        {

            return View();
        }
    }
}
