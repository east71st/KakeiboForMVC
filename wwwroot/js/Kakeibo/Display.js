class Display {

    /**
     * 検索開始日デートボックス
     */
    #firstDate = null;

    /**
     * コンストラクタ
     */
    constructor() { };

    /**
     * 初期化
     */
    init() {
        this.#firstDate = document.getElementById('firstDate');

        // 初期化の最終処理
        this.#windowOnLoad();
    }

    /**
    * 初期化の最後処理
    */
    #windowOnLoad() {
        this.#firstDate.focus();
    }
}

window.onload = function () {
    let obj = new Display;
    obj.init()
};