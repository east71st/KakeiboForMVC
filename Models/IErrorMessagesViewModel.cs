namespace KakeiboForMVC.Models
{
    /// <summary>
    /// エラーメッセージビューモデルインターフェース
    /// </summary>
    public interface IErrorMessagesViewModel
    {
        List<string> ErrorMessages { get; set; }
    }
}
