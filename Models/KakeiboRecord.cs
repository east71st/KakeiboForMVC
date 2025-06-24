namespace KakeiboForMVC.Models
{
    public class KakeiboRecord
    {
        /// <summary>
        /// 家計簿ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 日付
        /// </summary>
        public DateTime Hiduke { get; set; }

        /// <summary>
        /// 費目ID
        /// </summary>
        public int HimokuId { get; set; }

        /// <summary>
        /// 費目名
        /// </summary>
        public string? HimokuName { get; set; }

        /// <summary>
        /// 明細
        /// </summary>
        public string? Meisai { get; set; }

        /// <summary>
        /// 入金額
        /// </summary>
        public decimal? NyukinGaku { get; set; }

        /// <summary>
        /// 出金額
        /// </summary>
        public decimal? ShukinGaku { get; set; }
    }
}
