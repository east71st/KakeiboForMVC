using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace KakeiboForMVC.Models
{
    public class UpdateViewModel
    {
        /// <summary>
        /// 検索開始期間
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? FirstDate { get; set; }

        /// <summary>
        /// 検索終了期間
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? LastDate { get; set; }

        /// <summary>
        /// 検索費目ID
        /// </summary>
        public int? HimokuId { get; set; }

        /// <summary>
        /// 検索明細
        /// </summary>
        public string? Meisai { get; set; }

        /// <summary>
        /// 表示家計簿データ
        /// </summary>
        public List<KakeiboRecord>? KakeiboList { get; set; }

        /// <summary>
        /// 費目名のセレクトリスト
        /// </summary>
        public List<SelectListItem>? HimokuNameSelect { get; set; }
    }
}
