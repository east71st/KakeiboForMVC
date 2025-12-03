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
        public static readonly int MaxDisplayKakeiboList = 1000;

        /// <summary>
        /// 年月日表示用書式"yyyy年MM月dd日"
        /// </summary>
        public static readonly string DisplayDateFormat = "yyyy年MM月dd日";

        /// <summary>
        /// 年月日略式表示用書式"yyyy/MM/dd"
        /// </summary>
        public static readonly string DisplayShortDateFormat = "yyyy/MM/dd";

        /// <summary>
        /// 年月表示用書式"yyyy年MM月"
        /// </summary>
        public static readonly string DisplayMonthFormat = "yyyy年MM月";

        /// <summary>
        /// 年月日データ用書式"yyyy-MM-dd"
        /// </summary>
        public static readonly string DataDateFormat = "yyyy-MM-dd";

        /// <summary>
        /// 年月日略式データ用書式"yyyyMMdd"
        /// </summary>
        public static readonly string DataDateShortFormat = "yyyyMMdd";

        /// <summary>
        /// 数値表示用書式"###,###"
        /// </summary>
        public static readonly string DisplayNumberFormat = "###,###";

        /// <summary>
        /// 並び順の表示文字列
        /// </summary>
        public static readonly Dictionary<int, string> SortSymbolDict = new()
        {
            [1] = "▲",
            [2] = "▼",
            [-1] = "△",
            [-2] = "▽"
        };
    }
}
