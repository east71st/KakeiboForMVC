using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace KakeiboForMVC.Models
{
    /// <summary>
    /// 費目別集計画面
    /// </summary>
    public class HimokuCompileViewModel : ICommonCompileViewModel, IErrorMessagesViewModel
    {
        /// <summary>
        /// 集計開始月
        /// </summary>
        [Display(Name = "集計開始月")]
        [Required(ErrorMessage = "{0}が未設定です。")]
        public string? FirstMonth { get; set; }

        /// <summary>
        /// 集計最終月
        /// </summary>
        [Display(Name = "集計最終月")]
        [Required(ErrorMessage = "{0}が未設定です。")]
        public string? LastMonth { get; set; }

        /// <summary>
        /// 費目ID
        /// </summary>
        [Display(Name = "費目")]
        [Required(ErrorMessage = "{0}が未設定です。")]
        public int? HimokuId { get; set; }

        /// <summary>
        /// 費目名
        /// </summary>
        public string? HimokuName { get; set; }

        /// <summary>
        /// 集計開始月のセレクトリスト
        /// </summary>
        public List<SelectListItem> FirstMonthSelect { get; set; } = [];

        /// <summary>
        /// 集計最終月のセレクトリスト
        /// </summary>
        public List<SelectListItem> LastMonthSelect { get; set; } = [];

        /// <summary>
        /// 費目名のセレクトリスト
        /// </summary>
        public List<SelectListItem> HimokuNameSelect { get; set; } = [];

        /// <summary>
        /// 家計簿集計テーブル
        /// </summary>
        public DataTable CompileKakeiboTable { get; set; } = new DataTable();

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public List<string> ErrorMessages { get; set; } = [];
    }
}
