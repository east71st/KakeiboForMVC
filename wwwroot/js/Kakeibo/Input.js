class Input {

    /**
     * 日付デートボックス
     */
    #hidukeDate = null;

    /**
     * コンストラクタ
     */
    constructor() { };

    /**
     * 初期化
     */
    init() {
        this.#hidukeDate = document.getElementById('hidukeDate');

        // 初期化の最終処理
        this.#windowOnLoad();
    }

    /**
    * 初期化の最後処理
    */
    #windowOnLoad() {
        this.#hidukeDate.focus();
    }
}

window.onload = function () {
    let obj = new Input;
    obj.init()
};