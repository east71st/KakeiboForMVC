class Input {

    /**
     * 日付デートボックス
     */
    #hidukeDate = null;

    /**
     * 費目IDセレクトボックス
     */
    #himokuId = null;

    /**
     * プリントタグ
     */
    #print = null;
    /**
     * 印刷可能範囲
     */
    #printbleArea = null;

    /**
     * 明細リスト
     */
    #meisaiList = null;

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
        this.#hidukeDate = document.getElementById('hidukeDate');
        this.#himokuId = document.getElementById('himokuId');
        this.#meisaiList = document.getElementById('meisaiList');
        this.#print = document.getElementById('print');
        this.#printbleArea = document.getElementById('printableArea');
        this.#copy = document.getElementById('copy');
        this.#table = document.getElementById('table');

        this.#print.addEventListener('click', e => this.#printOnClick(e));
        this.#copy.addEventListener('click', e => this.#copyOnClick(e));
        this.#himokuId.addEventListener('change', e => this.#himokuIdOnChange(e));

        // 初期化の最終処理
        this.#windowOnLoad();
    }

    /**
     * プリント
     * @param {Event} e
     */
    #printOnClick(e) {
        const printContents = this.#printbleArea.innerHTML;
        const originalContents = document.body.innerHTML;

        document.body.innerHTML = printContents;
        window.print();
        document.body.innerHTML = originalContents;
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
     * 費目ID変更
     * @param {Event} e
     */
    #himokuIdOnChange(e) {
        this.#getMeisaiList(e.target.value);
    }

    /**
     * 明細履歴リスト取得
     * @param {String} himokuIdValue
     */
    async #getMeisaiList(himokuIdValue) {
        try {
            const response = await fetch('/Kakeibo/GetMeisaiList/', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json', },
                body: JSON.stringify({ himokuId: himokuIdValue, }),
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const responseData = await response.json();

            const x = responseData.meisaiList;
            this.#meisaiList.innerHTML = "";
            for (const item of responseData.meisaiList) {
                const option = document.createElement('option');
                option.value = item;
                this.#meisaiList.appendChild(option);
            };
        } catch (error) {
            alert('明細履歴リストの取得に失敗しました:', error);
        }
    }

    /**
    * 初期化の最後処理
    */
    async #windowOnLoad() {
        this.#getMeisaiList(this.#himokuId.value);
        this.#hidukeDate.focus();
    }
}

window.onload = function () {
    let obj = new Input;
    obj.init()
};