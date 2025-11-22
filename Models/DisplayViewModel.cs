namespace KakeiboForMVC.Models
{
    /// <summary>
    /// 表示画面ビューモデル
    /// </summary>
    public class DisplayViewModel : CommonBaseViewModel
    {
        /// <summary>
        /// Idの並び順
        /// </summary>
        public int? SortId { get; set; }

        /// <summary>
        /// 日付の並び順
        /// </summary>
        public int? SortHiduke { get; set; }

        /// <summary>
        /// 費目Idの並び順
        /// </summary>
        public int? SortHimokuId { get; set; }

        /// <summary>
        /// 明細の並び順
        /// </summary>
        public int? SortMeisai { get; set; }
    }
}
