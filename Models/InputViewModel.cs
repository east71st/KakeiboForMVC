using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace KakeiboForMVC.Models
{
    public class InputViewModel
    {
        /// <summary>
        /// 日付
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? InputDate { get; set; }

        /// <summary>
        /// 費目ID
        /// </summary>
        public int? HimokuId { get; set; }

        /// <summary>
        /// 明細
        /// </summary>
        public string? Meisai { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        public Decimal? Kingaku { get; set; }

        /// <summary>
        /// 費目名のセレクトリスト
        /// </summary>
        public List<SelectListItem>? HimokuNameSelect { get; set; }

        /// <summary>
        /// 表示家計簿データ
        /// </summary>
        public List<KakeiboRecord>? KakeiboList { get; set; }
    }
}
