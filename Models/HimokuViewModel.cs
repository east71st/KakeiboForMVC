using System.ComponentModel.DataAnnotations;

namespace KakeiboForMVC.Models
{
    /// <summary>
    /// 費目ビューモデル
    /// </summary>
    public class HimokuViewModel : IErrorMessagesViewModel
    {
        /// <summary>
        /// 費目名
        /// </summary>
        [Display(Name = "費目名")]
        [StringLength(20, ErrorMessage = "{0}の文字数が{1}を超えています。")]
        public string? Name { get; set; }

        /// <summary>
        /// 費目データリスト
        /// </summary>
        public List<HimokuRecord> HimokuList { get; set; } = [];

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public List<string> ErrorMessages { get; set; } = [];
    }
}
