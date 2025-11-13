class Compile {

    /**
     * 集計開始月日セレクトボックス
     */
    #firstMonth = null;

    /**
     * プリントタグ
     */
    #print = null;
    /**
     * 印刷可能範囲
     */
    #printbleArea = null;
    /**
     * テーブルエリア
     */
    #tableArea = null

    /**
     * コピー
     */
    #copy = null;
    /**
     * テーブル
     */
    #table = null;

    /**
     * コンストラクタ
     */
    constructor() { };

    /**
     * 初期化
     */
    init() {
        this.#firstMonth = document.getElementById('firstMonth');
        this.#print = document.getElementById('print');
        this.#printbleArea = document.getElementById('printableArea');
        this.#tableArea = document.getElementById('table-area');
        this.#copy = document.getElementById('copy');
        this.#table = document.getElementById('table');

        this.#print.addEventListener('click', e => this.#printOnClick(e));
        this.#copy.addEventListener('click', e => this.#copyOnClick(e));

        // 初期化の最終処理
        this.#windowOnLoad();
    }

    /**
     * プリント
     * @param {Event} e
     */
    #printOnClick(e) {
        this.#tableArea.style.overflow = "visible";
        const printContents = this.#printbleArea.innerHTML;
        const originalContents = document.body.innerHTML;

        document.body.innerHTML = printContents;
        window.print();
        document.body.innerHTML = originalContents;
        this.#tableArea.style.overflow = "auto";
        location.reload();
    }

    /**
     * コピー
     * @param {Event} e
     */
    async #copyOnClick(e) {
        const table = this.#table.innerText;
        try {
            await navigator.clipboard.writeText(table);
            alert('クリップボードへコピーしました:');
        } catch (error) {
            alert('クリップボードへのコピーに失敗しました:', error);
        }
    }

    /**
    * 初期化の最後処理
    */
    #windowOnLoad() {
        // 初期表示フォーカス設定
        this.#firstMonth.focus();
    }
}

window.onload = function () {
    let obj = new Compile;
    obj.init()
};