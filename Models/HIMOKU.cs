using System.ComponentModel.DataAnnotations;

namespace KakeiboForMVC.Models
{
    public class HIMOKU
    {
        /// <summary>
        /// 費目ID
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 費目名
        /// </summary>
        [StringLength(20)]
        public required string NAME { get; set; }
    }
}
