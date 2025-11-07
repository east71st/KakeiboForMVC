using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KakeiboForMVC.Models
{
    /// <summary>
    /// 修正画面ビューモデル
    /// </summary>
    public class UpdateViewModel : DisplayViewModel
    {
        /// <summary>
        /// 家計簿ID
        /// </summary>
        [Display(Name = "ID")]
        [Required(ErrorMessage = "{0}が未入力です。")]
        public int? UpdateId { get; set; }

        /// <summary>
        /// 日付
        /// </summary>
        [Display(Name = "日付")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0}が未設定です。")]
        public DateTime? UpdateHiduke { get; set; }

        /// <summary>
        /// 費目ID
        /// </summary>
        [Display(Name = "費目")]
        [Required(ErrorMessage = "{0}が未設定です。")]
        public int? UpdateHimokuId { get; set; }

        /// <summary>
        /// 明細
        /// </summary>
        [Display(Name = "明細")]
        [StringLength(100, ErrorMessage = "{0}の文字数が{1}を超えています。")]
        public string? UpdateMeisai { get; set; }

        /// <summary>
        /// 入金額
        /// </summary>
        [Display(Name = "収入")]
        [Column(TypeName = "decimal(18, 0)")]
        [Range(0, 999999999999999999, ErrorMessage = "{0}が範囲外です。{1}～{2}の整数を入力して下さい。")]
        public decimal? UpdateNyukinGaku { get; set; }

        /// <summary>
        /// 出金額
        /// </summary>
        [Display(Name = "支出")]
        [Column(TypeName = "decimal(18, 0)")]
        [Range(0, 999999999999999999, ErrorMessage = "{0}が範囲外です。{1}～{2}の整数を入力して下さい。")]
        public decimal? UpdateShukinGaku { get; set; }

        /// <summary>
        /// 確認ダイアログ表示フラグ
        /// </summary>
        public bool ShowDialog { get; set; } = false;
    }
}
