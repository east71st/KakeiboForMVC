using System.ComponentModel.DataAnnotations;

namespace KakeiboForMVC.Models
{
    /// <summary>
    /// 費目テーブルの行データ
    /// </summary>
    public class HimokuRecord
    {
        /// <summary>
        /// 費目ID
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 費目名
        /// </summary>
        public string? Name { get; set; }
    }
}
