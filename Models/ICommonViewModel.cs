using Microsoft.AspNetCore.Mvc.Rendering;

namespace KakeiboForMVC.Models
{
    /// <summary>
    /// 共通ビューモデルインターフェース
    /// </summary>
    public interface ICommonViewModel
    {
        /// <summary>
        /// 費目名のセレクトリスト
        /// </summary>
        List<SelectListItem> HimokuNameSelect { get; set; }

        /// <summary>
        /// 明細履歴のリスト
        /// </summary>
        List<string> MeisaiList { get; set; }

        /// <summary>
        /// 家計簿データリスト
        /// </summary>
        List<KakeiboRecord> KakeiboList { get; set; }
    }
}
