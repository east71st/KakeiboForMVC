class Input {

    /**
     * 日付デートボックス
     */
    #hidukeDate = null;

    /**
     * 印刷可能範囲
     */
    #printbleArea = null;
    /**
     * プリントタグ
     */
    #printTag = null;

    /**
     * コンストラクタ
     */
    constructor() { };

    /**
     * 初期化
     */
    init() {
        this.#hidukeDate = document.getElementById('hidukeDate');
        this.#printTag = document.getElementById('printTag');
        this.#printbleArea = document.getElementById('printableArea');

        this.#printTag.addEventListener('click', e => this.#printTagOnClick(e));

        // 初期化の最終処理
        this.#windowOnLoad();
    }

    /**
     * プリント
     * @param {Event} e
     */
    #printTagOnClick(e) {
        const printContents = this.#printbleArea.innerHTML;
        const originalContents = document.body.innerHTML;

        document.body.innerHTML = printContents;
        window.print();
        document.body.innerHTML = originalContents;
        location.reload();
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