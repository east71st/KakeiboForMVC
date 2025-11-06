using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KakeiboForMVC.Models
{
    /// <summary>
    /// 入力画面ビューモデル
    /// </summary>
    public class InputViewModel : IErrorMessagesViewModel, ICommonViewModel
    {
        /// <summary>
        /// 日付
        /// </summary>
        [Display(Name = "日付")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0}が未入力です。")]
        public DateTime? Hiduke { get; set; }

        /// <summary>
        /// 費目ID
        /// </summary>
        [Display(Name = "費目")]
        [Required(ErrorMessage = "{0}が未設定です。")]
        public int? HimokuId { get; set; }

        /// <summary>
        /// 明細
        /// </summary>
        [Display(Name = "明細")]
        [StringLength(100, ErrorMessage = "{0}の文字数が{1}を超えています。")]
        public string? Meisai { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        [Display(Name = "金額")]
        [Column(TypeName = "decimal(18, 0)")]
        [Range(0, 999999999999999999, ErrorMessage = "{0}が範囲外です。{1}～{2}の整数を入力して下さい。")]
        public Decimal? Kingaku { get; set; }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public List<string> ErrorMessages { get; set; } = [];

        /// <summary>
        /// 費目名のセレクトリスト
        /// </summary>
        public List<SelectListItem> HimokuNameSelect { get; set; } = [];

        /// <summary>
        /// 家計簿テーブルリスト
        /// </summary>
        public List<KakeiboRecord> KakeiboList { get; set; } = [];
    }
}
