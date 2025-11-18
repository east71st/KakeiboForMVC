using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Input([FromQuery] HimokuViewModel viewModel)
        {
            // -----------------------入力チェック開始-----------------------

            var isError = false;

            // バリデーションチェック
            ModelState.Remove(nameof(viewModel.UpdateId));
            ModelState.Remove(nameof(viewModel.UpdateName));
            if (!ModelState.IsValid)
            {
                isError = true;
            }

            // 同じ費目名が既に登録されている場合は、エラー
            var result = await _context.HIMOKU.
                Where(x => x.NAME == viewModel.Name).FirstOrDefaultAsync();
            if (result != null)
            {
                ModelState.AddModelError(string.Empty,
                    $"{viewModel.Name}は既に登録されています。");
                isError = true;
            }

            if (isError)
            {
                // 費目テーブルの取得
                viewModel = await GetDisplayViewModel(viewModel);
                // エラーメッセージの取得
                viewModel.ErrorMessages.AddRange(Common.GetErrorMessage(ModelState));
                return View(nameof(Update), viewModel);
            }

            // -----------------------入力チェック終了-----------------------

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
        public async Task<IActionResult> Update([FromQuery] HimokuViewModel viewModel)
        {
            // -----------------------入力チェック開始-----------------------

            var isError = false;

            // バリデーションチェック
            ModelState.Remove(nameof(viewModel.Name));
            if (!ModelState.IsValid)
            {
                isError = true;
            }

            // 選択したデータが費目データに存在しない場合はエラー
            var result = await _context.HIMOKU.
                Where(x => x.ID == viewModel.UpdateId).FirstOrDefaultAsync();
            if (result == null)
            {
                ModelState.AddModelError(string.Empty,
                    "選択したデータが存在しません。既に、削除された可能性があります。");
                isError = true;
            }

            if (isError)
            {
                // 費目テーブルの取得
                viewModel = await GetDisplayViewModel(viewModel);
                // エラーメッセージの取得
                viewModel.ErrorMessages.AddRange(Common.GetErrorMessage(ModelState));
                return View(nameof(Update), viewModel);
            }

            // -----------------------入力チェック終了-----------------------

            result!.ID = viewModel.UpdateId!.Value;
            result.NAME = viewModel.UpdateName!;

            _context.Update(result);
            await _context.SaveChangesAsync();

            // 費目テーブルの取得
            viewModel = await GetDisplayViewModel(viewModel);

            return View(viewModel);
        }

        /// <summary>
        /// 削除処理
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete([FromQuery] HimokuViewModel viewModel)
        {
            // -----------------------入力チェック開始-----------------------

            var isError = false;

            ModelState.Remove(nameof(viewModel.Name));
            ModelState.Remove(nameof(viewModel.UpdateName));
            if (!ModelState.IsValid)
            {
                isError = true;
            }

            // 選択した費目データが家計簿データに存在する場合はエラー
            var list = await _context.KAKEIBO.
                Where(x => x.HIMOKU_ID == viewModel.UpdateId).FirstOrDefaultAsync();
            if (list != null)
            {
                string? name = await _context.HIMOKU.
                    Where(x => x.ID == viewModel.UpdateId).
                    Select(x => x.NAME).FirstOrDefaultAsync();

                ModelState.AddModelError(string.Empty,
                    $"{name}は家計簿データで使われています。削除できません。");
                isError = true;
            }

            // 選択したデータが費目データに存在しない場合はエラー
            var result = await _context.
                HIMOKU.Where(x => x.ID == viewModel.UpdateId).FirstOrDefaultAsync();
            if (result == null)
            {
                ModelState.AddModelError(string.Empty, 
                    "選択したデータが存在しません。既に、削除された可能性があります。");
                isError = true;
            }

            if (isError)
            {
                // 費目テーブルの取得
                viewModel = await GetDisplayViewModel(viewModel);
                // エラーメッセージの取得
                viewModel.ErrorMessages.AddRange(Common.GetErrorMessage(ModelState));
                return View(nameof(Update), viewModel);
            }

            // -----------------------入力チェック終了-----------------------

            // 確認ダイアログが表示されていない場合は、表示フラグを立てて再表示
            if (!viewModel.ShowDialog)
            {
                ModelState.Remove(nameof(viewModel.ShowDialog));
                viewModel.ShowDialog = true;

                // 費目名セレクトリストと家計簿テーブルの取得
                viewModel = await GetDisplayViewModel(viewModel);
                return View(nameof(Update), viewModel);
            }

            _context.HIMOKU.Remove(result!);
            await _context.SaveChangesAsync();

            ModelState.Remove(nameof(viewModel.ShowDialog));
            viewModel.ShowDialog = false;
            // 費目名セレクトリストと家計簿テーブルの取得
            viewModel = await GetDisplayViewModel(viewModel);

            return View(nameof(Update), viewModel);
        }

        /// <summary>
        /// 費目テーブルの取得
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private async Task<HimokuViewModel> GetDisplayViewModel(
            HimokuViewModel viewModel)
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
    }
}
