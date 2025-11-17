using System.ComponentModel.DataAnnotations;

namespace KakeiboForMVC.Models
{
    /// <summary>
    /// 表示画面のCSVダウンロードデータJISON変換用モデル
    /// </summary>
    public class DownloadCsvDisplayModel
    {
        public DateTime? FirstDate { get; set; }

        public DateTime? LastDate { get; set; }

        public int? HimokuId { get; set; }

        public  string? HimokuName { get; set; }

        public string? Meisai { get; set; }
    }
}
