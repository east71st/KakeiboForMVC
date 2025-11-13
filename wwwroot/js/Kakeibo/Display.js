class Display {

    /**
     * 送信フォーム
     */
    #tempDataForm = null;
    /**
     * 検索開始日デートボックス
     */
    #firstDate = null;
    /**
     * セッションストレージに保管する入力ボックス
     */
    #tempElements = null;
    /**
     * ゲットからのレスポンスか否か
     */
    #isGet = null;

    /**
     * プリント
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
     * セッションストレージに保管するデータのキー名
     */
    #tempDataName = this.constructor.name + 'TempData';

    /**
     * コンストラクタ
     */
    constructor() { };

    /**
     * 初期化
     */
    init() {
        this.#tempDataForm = document.getElementById('tempDataForm');
        this.#firstDate = document.getElementById('firstDate');
        this.#tempElements = document.querySelectorAll('.tempElements');
        this.#isGet = document.getElementById('isGet');
        this.#print = document.getElementById('print');
        this.#printbleArea = document.getElementById('printableArea');
        this.#tableArea = document.getElementById('table-area');
        this.#copy = document.getElementById('copy');
        this.#table = document.getElementById('table');

        this.#tempDataForm.addEventListener('submit', e => this.#formOnSubmit(e));
        this.#print.addEventListener('click', e => this.#printOnClick(e));
        this.#copy.addEventListener('click', e => this.#copyOnClick(e));

        // 初期化の最終処理
        this.#windowOnLoad();
    }

    /**
     * データが送信されたときの処理
     * @param {event} e
     */
    #formOnSubmit(e) {
        // 入力データをセッションストレージに保管
        this.#setTempData();
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
            alert('クリップボードへコピーしました。');
        } catch (error) {
            alert(`クリップボードへのコピーに失敗しました:${error}`);
        }
    }

    /**
     * 入力データをセッションストレージに保管
     */
    #setTempData() {
        const map = new Map();
        this.#tempElements.forEach(x => map.set(x.name, x.value));
        sessionStorage.setItem(
            this.#tempDataName, JSON.stringify(Object.fromEntries(map)));
    }

    /**
     * セッションストレージにデータが保管されている場合はデータを取得して、入力ボックスに設定
     */
    #getTempData() {
        if (sessionStorage.getItem(this.#tempDataName) != null) {
            const map = new Map(
                Object.entries(JSON.parse(sessionStorage.getItem(this.#tempDataName))));
            this.#tempElements.forEach(x => x.value = map.get(x.name));
        }
    }

    /**
    * 初期化の最後処理
    */
    #windowOnLoad() {
        // ゲットからのレスポンスでセッションストレージにデータが保管せれている場合は再表示
        if (this.#isGet.value === 'True') {
            // セッションストレージにデータが保管されている場合はデータを取得して、入力ボックスに設定
            this.#getTempData();
            this.#tempDataForm.submit();
        }
        // 初期表示フォーカス設定
        this.#firstDate.focus();
    }
}

window.onload = function () {
    let obj = new Display;
    obj.init()
};