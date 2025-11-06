using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KakeiboForMVC.Models
{
    /// <summary>
    /// 家計簿テーブル
    /// </summary>
    public class KAKEIBO
    {
        /// <summary>
        /// 家計簿ID
        /// </summary>
        [Key]
        [Required]
        public required int ID { get; set; }

        /// <summary>
        /// 日付
        /// </summary>
        [DataType(DataType.Date)]
        [Required]
        public required DateTime HIDUKE { get; set; }

        /// <summary>
        /// 費目ID
        /// </summary>
        [Required]
        public required int HIMOKU_ID { get; set; }

        /// <summary>
        /// 明細
        /// </summary>
        [StringLength(100)]
        public string? MEISAI {  get; set; }

        /// <summary>
        /// 入金額
        /// </summary>
        [Column(TypeName = "decimal(18, 0)")]
        [Range(1, 999999999999999999)]
        public decimal? NYUKINGAKU { get; set; }

        /// <summary>
        /// 出金額
        /// </summary>
        [Column(TypeName = "decimal(18, 0)")]
        [Range(1, 999999999999999999)]
        public decimal? SHUKINGAKU { get; set; }
    }
}
