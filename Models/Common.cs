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
        public static int MaxDisplayKakeiboList { get; set; } = 200;

        /// <summary>
        /// 集計開始月と最終月の表示用書式
        /// </summary>
        public static string DisplayMonthFormat { get; set; } = "yyyy年MM月";

        /// <summary>
        /// 集計開始月と最終月のデータ用書式
        /// </summary>
        public static string DataMonthFormat { get; set; } = "yyyy-MM-dd";
    }
}
