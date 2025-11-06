using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using KakeiboForMVC.Data;
using KakeiboForMVC.Models;

namespace KakeiboForMVC.Controllers
{
    /// <summary>
    /// 費目コントローラー
    /// </summary>
    public class HimokuController : Controller
    {
        private readonly ILogger<HimokuController> _logger;
        private readonly KakeiboForMVCContext _context;

        /// <summary>
        /// 家計簿コントローラ　コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        public HimokuController(ILogger<HimokuController> logger,
            KakeiboForMVCContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// 初期表示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Input()
        {
            HimokuViewModel viewModel = new();

            // 費目テーブルの取得
            viewModel = await GetDisplayViewModel(viewModel);

            return View(nameof(Update), viewModel);
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Input([FromForm] HimokuViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // エラーメッセージの取得
                viewModel.ErrorMessages.AddRange(Common.GetErrorMessage(ModelState));
                return View(nameof(Update), viewModel);
            }

            HIMOKU himoku = new()
            {
                ID = 0,
                NAME = viewModel.Name!,
            };

            _context.Add(himoku);
            await _context.SaveChangesAsync();

            // 費目テーブルの取得
            viewModel = await GetDisplayViewModel(viewModel);

            return View(nameof(Update), viewModel);
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update([FromQuery] HimokuUpdateViewModel viewModel)
        {
            // -----------------------入力チェック開始-----------------------

            // 選択したデータが存在しない場合はエラー
            var result = await _context.KAKEIBO.FindAsync(viewModel.UpdateId);
            if (result == null)
            {
                ModelState.AddModelError(string.Empty, "選択したデータが存在しません。既に、削除された可能性があります。");

                // エラーメッセージの取得
                viewModel.ErrorMessages.AddRange(Common.GetErrorMessage(ModelState));
                return View(viewModel);
            }

            // 表示条件に関するデータのバリデーションチェック
            if (ModelState.GetFieldValidationState(nameof(viewModel.Name)) == ModelValidationState.Invalid)
            {
                // エラーメッセージの取得
                viewModel.ErrorMessages.AddRange(Common.GetErrorMessage(ModelState));
                return View(viewModel);
            }

            // バリデーションチェック
            if (!ModelState.IsValid)
            {
                // エラーメッセージの取得
                viewModel.ErrorMessages.AddRange(Common.GetErrorMessage(ModelState));
                return View(viewModel);
            }

            // -----------------------入力チェック終了-----------------------

            HIMOKU Himoku = new()
            {
                ID = viewModel.UpdateId!.Value,
                NAME = viewModel.UpdateName!,
            };

            _context.Update(Himoku);
            await _context.SaveChangesAsync();

            // 費目テーブルの取得
            viewModel = (HimokuUpdateViewModel)await GetDisplayViewModel(viewModel);

            return View(viewModel);
        }

        /// <summary>
        /// 費目テーブルの取得
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private async Task<HimokuViewModel> GetDisplayViewModel(HimokuViewModel viewModel)
        {
            var query = _context.
                HIMOKU.OrderBy(x => x.ID).
                Select(x => x);

            foreach (var item in await query.ToListAsync())
            {
                HimokuRecord record = new()
                {
                    Id = item.ID,
                    Name = item.NAME,
                };
                viewModel.HimokuList.Add(record);
            }

            return viewModel;
        }

        /// <summary>
        /// 削除処理
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete([FromQuery] HimokuUpdateViewModel viewModel)
        {
            // 選択したデータが存在しない場合はエラー
            var result = await _context.HIMOKU.FindAsync(viewModel.UpdateId);
            if (result == null)
            {
                ModelState.AddModelError(string.Empty, "選択したデータが存在しません。既に、削除された可能性があります。");

                // エラーメッセージの取得
                viewModel.ErrorMessages.AddRange(Common.GetErrorMessage(ModelState));
                return View(nameof(Update), viewModel);
            }

            _context.HIMOKU.Remove(result);
            await _context.SaveChangesAsync();

            // 費目名セレクトリストと家計簿テーブルの取得
            viewModel = (HimokuUpdateViewModel)await GetDisplayViewModel(viewModel);

            return View(nameof(Update), viewModel);
        }
    }
}
