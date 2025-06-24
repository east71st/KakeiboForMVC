using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KakeiboForMVC.Models
{
    public class KAKEIBO
    {
        /// <summary>
        /// 家計簿ID
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 日付
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime HIDUKE { get; set; }

        /// <summary>
        /// 費目D
        /// </summary>
        public int HIMOKU_ID { get; set; }

        /// <summary>
        /// 明細
        /// </summary>
        [StringLength(100)]
        public string? MEISAI {  get; set; }

        /// <summary>
        /// 入金額
        /// </summary>
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? NYUKINGAKU { get; set; }

        /// <summary>
        /// 出金額
        /// </summary>
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? SHUKINGAKU { get; set; }
    }
}
