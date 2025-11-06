using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KakeiboForMVC.Models
{
    /// <summary>
    /// 表示画面ビューモデル
    /// </summary>
    public class DisplayViewModel : IErrorMessagesViewModel, ICommonViewModel
    {
        /// <summary>
        /// 検索開始日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "検索開始日")]
        [Required(ErrorMessage = "{0}が未入力です。")]
        public DateTime? FirstDate { get; set; }

        /// <summary>
        /// 検索最終日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "検索最終日")]
        [Required(ErrorMessage = "{0}が未入力です。")]
        public DateTime? LastDate { get; set; }

        /// <summary>
        /// 費目ID
        /// </summary>
        public int? HimokuId { get; set; }

        /// <summary>
        /// 費目名
        /// </summary>
        public string? HimokuName { get; set; }

        /// <summary>
        /// 明細
        /// </summary>
        public string? Meisai { get; set; }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public List<string> ErrorMessages { get; set; } = [];

        /// <summary>
        /// 費目名のセレクトリスト
        /// </summary>
        public List<SelectListItem> HimokuNameSelect { get; set; } = [];

        /// <summary>
        /// 家計簿データリスト
        /// </summary>
        public List<KakeiboRecord> KakeiboList { get; set; } = [];
    }
}
