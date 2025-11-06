using System.ComponentModel.DataAnnotations;

namespace KakeiboForMVC.Models
{
    /// <summary>
    /// 費目修正ビューモデル
    /// </summary>
    public class HimokuUpdateViewModel : HimokuViewModel
    {
        /// <summary>
        /// 費目ID
        /// </summary>
        [Display(Name = "ID")]
        [Required(ErrorMessage = "{0}が未入力です。")]
        public int? UpdateId { get; set; }

        /// <summary>
        /// 費目名
        /// </summary>
        [Required(ErrorMessage = "{0}が未入力です。")]
        [Display(Name = "費目名")]
        [StringLength(20, ErrorMessage = "{0}の文字数が{1}を超えています。")]
        public string? UpdateName { get; set; }
    }
}
