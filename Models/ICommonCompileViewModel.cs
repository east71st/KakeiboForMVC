using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KakeiboForMVC.Models
{
    /// <summary>
    /// 集計画面共通ビューモデルインターフェース
    /// </summary>
    public interface ICommonCompileViewModel
    {
        /// <summary>
        /// 集計開始月
        /// </summary>
        public string? FirstMonth { get; set; }

        /// <summary>
        /// 集計最終月
        /// </summary>
        public string? LastMonth { get; set; }

        /// <summary>
        /// 集計開始月のセレクトリスト
        /// </summary>
        public List<SelectListItem> FirstMonthSelect { get; set; }

        /// <summary>
        /// 集計最終月のセレクトリスト
        /// </summary>
        public List<SelectListItem> LastMonthSelect { get; set; }

        /// <summary>
        /// 家計簿集計テーブル
        /// </summary>
        public DataTable CompileKakeiboTable { get; set; }
    }
}
