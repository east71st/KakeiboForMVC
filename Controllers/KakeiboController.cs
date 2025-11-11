using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KakeiboForMVC.Data;
using KakeiboForMVC.Models;

namespace KakeiboForMVC.Controllers
{
    /// <summary>
    /// 家計簿コントローラ
    /// </summary>
    public class KakeiboController : Controller
    {
        private readonly ILogger<KakeiboController> _logger;
        private readonly KakeiboForMVCContext _context;

        /// <summary>
        /// 家計簿コントローラ　コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        public KakeiboController(ILogger<KakeiboController> logger,
            KakeiboForMVCContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return Redirect("Kakeibo/Display");
        }

        /// <summary>
        /// 入力画面　初期表示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Input()
        {
            InputViewModel viewModel = new()
            {
                Hiduke = DateTime.Now,
                HimokuId = 1,
            };

            // 費目名セレクトリストと家計簿テーブルの取得
            await GetInputViewModelData(_context, viewModel);

            return View(viewModel);
        }

        /// <summary>
        /// 入力画面　登録処理
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Input([FromForm] InputViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // 費目名セレクトリストと家計簿テーブルの取得
                await GetInputViewModelData(_context, viewModel);
                // エラーメッセージの取得
                viewModel.ErrorMessages.AddRange(Common.GetErrorMessage(ModelState));
                return View(viewModel);
            }

            KAKEIBO kakeibo = new()
            {
                ID = 0,
                HIDUKE = viewModel.Hiduke!.Value,
                HIMOKU_ID = viewModel.HimokuId!.Value,
                MEISAI = viewModel.Meisai,
            };

            // 費目IDが1：収入の場合は入金額、そうでなければ出金額
            if (viewModel.HimokuId == 1)
            {
                kakeibo.NYUKINGAKU = viewModel.Kingaku ?? 0;
                kakeibo.SHUKINGAKU = 0;
            }
            else
            {
                kakeibo.NYUKINGAKU = 0;
                kakeibo.SHUKINGAKU = viewModel.Kingaku ?? 0;
            }

            _context.Add(kakeibo);
            await _context.SaveChangesAsync();

            // 費目名セレクトリストと家計簿テーブルの取得
            await GetInputViewModelData(_context, viewModel);

            return View(viewModel);
        }

        /// <summary>
        /// 表示画面　初期表示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Display()
        {
            var year = DateTime.Today.Year;
            var month = DateTime.Today.Month;
            DisplayViewModel viewModel = new()
            {
                FirstDate = new DateTime(year, month, 1),
                LastDate = new DateTime(year, month, DateTime.DaysInMonth(year, month)),
                HimokuNameSelect = await GetHimokuNameSelect(_context),
            };

            // 費目名セレクトリストと家計簿テーブルの取得
            viewModel = await GetDisplayViewModel(viewModel);

            return View(viewModel);
        }

        /// <summary>
        /// 表示画面　検索表示
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Display([FromForm] DisplayViewModel viewModel)
        {
            // バリデーションチェック
            if (!ModelState.IsValid)
            {
                // 費目セレクトリストの取得
                viewModel.HimokuNameSelect = await GetHimokuNameSelect(_context);
                // エラーメッセージの取得
                viewModel.ErrorMessages.AddRange(Common.GetErrorMessage(ModelState));
                return View(viewModel);
            }

            // 費目名セレクトリストと家計簿テーブルの取得
            viewModel = await GetDisplayViewModel(viewModel);

            return View(viewModel);
        }

        /// <summary>
        /// 修正画面　初期表示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Update([FromQuery] UpdateViewModel viewModel)
        {
            var year = DateTime.Today.Year;
            var month = DateTime.Today.Month;
            viewModel.FirstDate ??= new DateTime(year, month, 1);
            viewModel.LastDate ??= new DateTime(year, month, DateTime.DaysInMonth(year, month));

            // 費目名セレクトリストの取得
            viewModel.HimokuNameSelect = await GetHimokuNameSelect(_context);

            // 費目名セレクトリストと家計簿テーブルの取得
            viewModel = (UpdateViewModel)await GetDisplayViewModel(viewModel);

            return View(viewModel);
        }

        /// <summary>
        /// 修正画面　更新処理
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update([FromQuery] UpdateViewModel viewModel, bool _)
        {
            // -----------------------入力チェック開始-----------------------

            // 選択したデータが存在しない場合はエラー
            var result = await _context.KAKEIBO.
                Where(x => x.ID == viewModel.UpdateId).FirstOrDefaultAsync();
            if (result == null)
            {
                ModelState.AddModelError(string.Empty, "選択したデータが存在しません。既に、削除された可能性があります。");

                // 費目セレクトリストの取得
                viewModel.HimokuNameSelect = await GetHimokuNameSelect(_context);
                // エラーメッセージの取得
                viewModel.ErrorMessages.AddRange(Common.GetErrorMessage(ModelState));
                return View(viewModel);
            }

            // 表示条件に関するデータのバリデーションチェック
            if (ModelState.GetFieldValidationState(nameof(viewModel.FirstDate)) == ModelValidationState.Invalid ||
                ModelState.GetFieldValidationState(nameof(viewModel.LastDate)) == ModelValidationState.Invalid ||
                ModelState.GetFieldValidationState(nameof(viewModel.HimokuId)) == ModelValidationState.Invalid ||
                ModelState.GetFieldValidationState(nameof(viewModel.Meisai)) == ModelValidationState.Invalid
                )
            {
                // 費目セレクトリストの取得
                viewModel.HimokuNameSelect = await GetHimokuNameSelect(_context);
                // エラーメッセージの取得
                viewModel.ErrorMessages.AddRange(Common.GetErrorMessage(ModelState));
                return View(viewModel);
            }

            bool isError = false;

            // バリデーションチェック
            if (!ModelState.IsValid)
            {
                isError = true;
            }

            // 費目が1：収入、かつ出金額が0以外の場合はエラー
            if (viewModel.UpdateHimokuId == 1 && viewModel.UpdateShukinGaku != 0)
            {
                ModelState.AddModelError(string.Empty, "費目が収入の場合、出金額は0でなければなりません。");
                isError = true;
            }

            // 費目が1：収入以外、かつ入金額0以外の場合はエラー
            if (viewModel.UpdateHimokuId != 1 && viewModel.UpdateNyukinGaku != 0)
            {
                ModelState.AddModelError(string.Empty, "費目が収入以外の場合、入金額は0でなければなりません。");
                isError = true;
            }

            if (isError)
            {
                // 費目名セレクトリストと家計簿テーブルの取得
                viewModel = (UpdateViewModel)await GetDisplayViewModel(viewModel);
                // エラーメッセージの取得
                viewModel.ErrorMessages.AddRange(Common.GetErrorMessage(ModelState));
                return View(viewModel);
            }

            // -----------------------入力チェック終了-----------------------

            result.ID = viewModel.UpdateId!.Value;
            result.HIDUKE = viewModel.UpdateHiduke!.Value;
            result.HIMOKU_ID = viewModel.UpdateHimokuId!.Value;
            result.MEISAI = viewModel.UpdateMeisai;
            result.NYUKINGAKU = viewModel.UpdateNyukinGaku;
            result.SHUKINGAKU = viewModel.UpdateShukinGaku;

            _context.Update(result);
            await _context.SaveChangesAsync();

            // 費目名セレクトリストと家計簿テーブルの取得
            viewModel = (UpdateViewModel)await GetDisplayViewModel(viewModel);

            return View(viewModel);
        }

        /// <summary>
        /// 修正画面　削除処理
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete([FromQuery] UpdateViewModel viewModel)
        {
            // 選択したデータが存在しない場合はエラー
            var result = await _context.KAKEIBO.
                Where(x => x.ID == viewModel.UpdateId).FirstOrDefaultAsync();
            if (result == null)
            {
                ModelState.AddModelError(string.Empty, "選択したデータが存在しません。既に、削除された可能性があります。");

                // 費目セレクトリストの取得
                viewModel.HimokuNameSelect = await GetHimokuNameSelect(_context);
                // エラーメッセージの取得
                viewModel.ErrorMessages.AddRange(Common.GetErrorMessage(ModelState));
                return View(nameof(Update), viewModel);
            }

            // 確認ダイアログが表示されていない場合は、表示フラグを立てて再表示
            if (!viewModel.ShowDialog)
            {
                ModelState.Remove(nameof(viewModel.ShowDialog));
                viewModel.ShowDialog = true;

                // 費目名セレクトリストと家計簿テーブルの取得
                viewModel = (UpdateViewModel)await GetDisplayViewModel(viewModel);
                return View(nameof(Update), viewModel);
            }

            _context.KAKEIBO.Remove(result);
            await _context.SaveChangesAsync();

            ModelState.Remove(nameof(viewModel.ShowDialog));
            viewModel.ShowDialog = false;
            // 費目名セレクトリストと家計簿テーブルの取得
            viewModel = (UpdateViewModel)await GetDisplayViewModel(viewModel);

            return View(nameof(Update), viewModel);
        }

        /// <summary>
        /// 集計画面　初期表示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Compile()
        {
            CompileViewModel viewModel = new();

            DateTime now = DateTime.Now;
            // 集計画面の集計開始月と最終月セレクトリストの取得
            await GetMonthSelect(_context, viewModel, now);

            // 集計開始月と最終月の初期設定
            viewModel.FirstMonth = now.AddDays(1 - now.Day).ToString(Common.DataMonthFormat);
            viewModel.LastMonth = now.AddDays(1 - now.Day).
                AddMonths(1).AddDays(-1).ToString(Common.DataMonthFormat);

            //家計簿テーブル作成
            await GetCompileKakeiboTable(_context, viewModel);

            return View(viewModel);
        }

        /// <summary>
        /// 集計画面　集計処理
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Compile(CompileViewModel viewModel)
        {
            DateTime now = DateTime.Now;
            // 集計画面の集計開始月と最終月セレクトリストの取得
            await GetMonthSelect(_context, viewModel, now);

            // バリデーションチェック
            if (!ModelState.IsValid)
            {
                // エラーメッセージの取得
                viewModel.ErrorMessages.AddRange(Common.GetErrorMessage(ModelState));

                return View(viewModel);
            }

            //家計簿テーブル作成
            await GetCompileKakeiboTable(_context, viewModel);

            return View(viewModel);
        }

        /// <summary>
        /// 家計簿テーブル作成
        /// </summary>
        /// <param name="context"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private static async Task GetCompileKakeiboTable(KakeiboForMVCContext context,
            CompileViewModel viewModel)
        {
            // 集計月リスト作成
            DateTime firstMonth = DateTime.Parse(viewModel.FirstMonth!);
            DateTime lastMonth = DateTime.Parse(viewModel.LastMonth!);

            List<string> monthList = [];
            DateTime date = firstMonth;
            while (date <= lastMonth.AddDays(1 - lastMonth.Day))
            {
                monthList.Add(date.ToString("yyyy年MM月"));
                date = date.AddMonths(1);
            }
            monthList.Add(date.ToString("合計"));

            // 費目名辞書の取得
            Dictionary<int, string> himokuDict = await GetHimokuNameDict(context);

            // 家計簿テーブルの列作成
            viewModel.CompileKakeiboTable.Columns.Add("費目名", typeof(string));

            foreach (var month in monthList)
            {
                viewModel.CompileKakeiboTable.Columns.
                    Add(month, typeof(Decimal));
            }

            // 家計簿テーブルの行作成と初期化
            DataRow row;
            foreach (var himoku in himokuDict)
            {
                row = viewModel.CompileKakeiboTable.NewRow();
                row["費目名"] = himoku.Value;
                foreach (var month in monthList)
                {
                    row[month] = 0;
                }
                viewModel.CompileKakeiboTable.Rows.Add(row);
            }
            row = viewModel.CompileKakeiboTable.NewRow();
            row["費目名"] = "支出計";
            foreach (var month in monthList)
            {
                row[month] = 0;
            }
            viewModel.CompileKakeiboTable.Rows.Add(row);
            row = viewModel.CompileKakeiboTable.NewRow();
            row["費目名"] = "収支";
            foreach (var month in monthList)
            {
                row[month] = 0;
            }
            viewModel.CompileKakeiboTable.Rows.Add(row);

            // 集計
            var result = await context.KAKEIBO.
                Where(x => x.HIDUKE >= firstMonth && x.HIDUKE <= lastMonth).ToListAsync();
            foreach (var item in result)
            {
                string month = item.HIDUKE.AddDays(1 - item.HIDUKE.Day).ToString("yyyy年MM月");
                Decimal cell = (Decimal)viewModel.CompileKakeiboTable.Rows[item.HIMOKU_ID - 1][month];
                Decimal sumCell = (Decimal)viewModel.CompileKakeiboTable.Rows[himokuDict.Count][month];
                Decimal balanceCell = (Decimal)viewModel.CompileKakeiboTable.Rows[himokuDict.Count + 1][month];
                Decimal totalCell = (Decimal)viewModel.CompileKakeiboTable.Rows[item.HIMOKU_ID - 1]["合計"];
                Decimal totalSumCell = (Decimal)viewModel.CompileKakeiboTable.Rows[himokuDict.Count]["合計"];
                Decimal TotalBalanceCell = (Decimal)viewModel.CompileKakeiboTable.Rows[himokuDict.Count + 1]["合計"];
                // 収入の場合
                if (item.HIMOKU_ID == 1)
                {
                    viewModel.CompileKakeiboTable.Rows[item.HIMOKU_ID - 1][month] = cell +
                        item.NYUKINGAKU ?? 0;
                    viewModel.CompileKakeiboTable.Rows[himokuDict.Count + 1][month] = balanceCell +
                        item.NYUKINGAKU ?? 0;

                    viewModel.CompileKakeiboTable.Rows[item.HIMOKU_ID - 1]["合計"] = totalCell +
                        item.NYUKINGAKU ?? 0;
                    viewModel.CompileKakeiboTable.Rows[himokuDict.Count + 1]["合計"] = TotalBalanceCell +
                        item.NYUKINGAKU ?? 0;
                }
                // 収入以外の場合
                else
                {
                    viewModel.CompileKakeiboTable.Rows[item.HIMOKU_ID - 1][month] = cell +
                        item.SHUKINGAKU ?? 0;
                    viewModel.CompileKakeiboTable.Rows[himokuDict.Count][month] = sumCell +
                        item.SHUKINGAKU ?? 0;
                    viewModel.CompileKakeiboTable.Rows[himokuDict.Count + 1][month] = balanceCell -
                        item.SHUKINGAKU ?? 0;

                    viewModel.CompileKakeiboTable.Rows[item.HIMOKU_ID - 1]["合計"] = totalCell +
                        item.SHUKINGAKU ?? 0;
                    viewModel.CompileKakeiboTable.Rows[himokuDict.Count]["合計"] = totalSumCell +
                        item.SHUKINGAKU ?? 0;
                    viewModel.CompileKakeiboTable.Rows[himokuDict.Count + 1]["合計"] = TotalBalanceCell -
                        item.SHUKINGAKU ?? 0;
                }

            }
        }

        /// <summary>
        /// 入力画面の費目名セレクトリストと家計簿テーブルの取得
        /// </summary>
        /// <param name="context"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private static async Task GetInputViewModelData(KakeiboForMVCContext context,
            InputViewModel viewModel)
        {
            viewModel.HimokuNameSelect = await GetHimokuNameSelect(context);

            var result = context.KAKEIBO.
                OrderByDescending(x => x.ID).Take(Common.MaxDisplayKakeiboList).Select(x => x);

            // 費目名辞書の取得
            var himokuNameDict = await GetHimokuNameDict(context);

            // 家計簿テーブルの取得
            foreach (var item in await result.ToListAsync())
            {
                KakeiboRecord record = new()
                {
                    Id = item.ID,
                    Hiduke = item.HIDUKE,
                    HimokuId = item.HIMOKU_ID,
                    HimokuName = GetHimokuName(item.HIMOKU_ID, himokuNameDict),
                    Meisai = item.MEISAI,
                    NyukinGaku = item.NYUKINGAKU,
                    ShukinGaku = item.SHUKINGAKU,
                };
                viewModel.KakeiboList.Add(record);
            }
        }

        /// <summary>
        /// 表示画面と修正画面の費目名と費目名セレクトリストと家計簿テーブルの取得
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private async Task<DisplayViewModel> GetDisplayViewModel(DisplayViewModel viewModel)
        {
            // 費目名セレクトリストの取得
            viewModel.HimokuNameSelect = await GetHimokuNameSelect(_context);

            // 費目名辞書の取得
            var himokuNameDict = await GetHimokuNameDict(_context);

            // 費目名の取得
            viewModel.HimokuName = GetHimokuName(viewModel.HimokuId, himokuNameDict);

            var result = _context.KAKEIBO.Select(x => x);

            //検索条件の適用
            if (viewModel.FirstDate != null)
            {
                result = result.Where(x => x.HIDUKE >= viewModel.FirstDate);
            }

            if (viewModel.LastDate != null)
            {
                result = result.Where(x => x.HIDUKE <= viewModel.LastDate);
            }

            if (viewModel.HimokuId != null &&
                himokuNameDict.ContainsKey(viewModel.HimokuId.Value))
            {
                result = result.Where(x => x.HIMOKU_ID == viewModel.HimokuId);
            }

            if (!string.IsNullOrWhiteSpace(viewModel.Meisai))
            {
                result = result.Where(x => !string.IsNullOrEmpty(x.MEISAI) &&
                    x.MEISAI.Contains(viewModel.Meisai));
            }

            result = result.OrderBy(x => x.HIDUKE).
                ThenBy(x => x.HIMOKU_ID).ThenBy(x => x.MEISAI);

            // 家計簿テーブルの取得
            foreach (var item in await result.ToListAsync())
            {
                KakeiboRecord record = new()
                {
                    Id = item.ID,
                    Hiduke = item.HIDUKE,
                    HimokuId = item.HIMOKU_ID,
                    HimokuName = GetHimokuName(item.HIMOKU_ID, himokuNameDict),
                    Meisai = item.MEISAI,
                    NyukinGaku = item.NYUKINGAKU,
                    ShukinGaku = item.SHUKINGAKU,
                };
                viewModel.KakeiboList.Add(record);
            }

            return viewModel;
        }

        /// <summary>
        /// 費目名セレクトリストの取得
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static async Task<List<SelectListItem>> GetHimokuNameSelect(KakeiboForMVCContext context)
        {
            return await context.
                HIMOKU.Select(x => new SelectListItem(x.NAME, x.ID.ToString())).
                ToListAsync();
        }

        /// <summary>
        /// 費目名辞書の取得
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static async Task<Dictionary<int, string>> GetHimokuNameDict(KakeiboForMVCContext context)
        {
            return await context.HIMOKU.ToDictionaryAsync(x => x.ID, x => x.NAME);
        }

        /// <summary>
        /// 費目名の取得
        /// </summary>
        /// <param name="himokuId"></param>
        /// <param name="himokuNameDict"></param>
        /// <returns></returns>
        private static string? GetHimokuName(int? himokuId, Dictionary<int, string> himokuNameDict)
        {
            return himokuId != null ?
                (himokuNameDict.TryGetValue(himokuId.Value, out string? name) ? name : null) :
                null;
        }

        /// <summary>
        /// 集計画面の集計開始月と最終月セレクトリストの取得
        /// </summary>
        /// <param name="context"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private static async Task GetMonthSelect(KakeiboForMVCContext context,
            CompileViewModel viewModel, DateTime now)
        {
            DateTime date = await context.KAKEIBO.Select(x => x.HIDUKE).MinAsync();
            date = date.AddDays(1 - date.Day);

            while (date <= now.AddDays(1 - now.Day))
            {
                viewModel.FirstMonthSelect.
                    Add(new SelectListItem(date.ToString(Common.DisplayMonthFormat),
                    date.ToString(Common.DataMonthFormat)));

                DateTime endMonth = date.AddMonths(1).AddDays(-1);
                viewModel.LastMonthSelect.
                    Add(new SelectListItem(endMonth.ToString(Common.DisplayMonthFormat),
                    endMonth.ToString(Common.DataMonthFormat)));

                date = date.AddMonths(1);
            }
        }
    }
}
