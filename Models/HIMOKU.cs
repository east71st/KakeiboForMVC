using System.ComponentModel.DataAnnotations;

namespace KakeiboForMVC.Models
{
    /// <summary>
    /// 費目テーブル
    /// </summary>
    public class HIMOKU
    {
        /// <summary>
        /// 費目ID
        /// </summary>
        [Key]
        public required int ID { get; set; }

        /// <summary>
        /// 費目名
        /// </summary>
        [StringLength(20)]
        public required string NAME { get; set; }
    }
}
