using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KakeiboForMVC.Models
{
    public static class Common
    {
        /// <summary>
        /// エラーメッセージの取得
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static List<string> GetErrorMessage(ModelStateDictionary modelState)
        {
            List<string> list = [];

            foreach (var value in modelState.Values)
            {
                if (value.Errors.Count == 0)
                {
                    continue;
                }
                else
                {
                    value.Errors.ToList().ForEach(x => list.Add(x.ErrorMessage));
                }
            }
            return list;
        }

        /// <summary>
        /// 表示画面の家計簿テーブル最大表示件数
        /// </summary>
        public static int MaxDisplayKakeiboList { get; set; } = 1000;

        /// <summary>
        /// 年月日表示用書式"yyyy年MM月dd日"
        /// </summary>
        public static string DisplayDateFormat { get; set; } = "yyyy年MM月dd日";

        /// <summary>
        /// 年月日略式表示用書式"yyyy/MM/dd"
        /// </summary>
        public static string DisplayShortDateFormat { get; set; } = "yyyy/MM/dd";

        /// <summary>
        /// 年月表示用書式"yyyy年MM月"
        /// </summary>
        public static string DisplayMonthFormat { get; set; } = "yyyy年MM月";

        /// <summary>
        /// 年月日データ用書式"yyyy-MM-dd"
        /// </summary>
        public static string DataDateFormat { get; set; } = "yyyy-MM-dd";

        /// <summary>
        /// 年月日略式データ用書式"yyyyMMdd"
        /// </summary>
        public static string DataDateShortFormat { get; set; } = "yyyyMMdd";

        /// <summary>
        /// 数値表示用書式"###,###"
        /// </summary>
        public static string DisplayNumberFormat { get; set; } = "###,###";
    }
}
