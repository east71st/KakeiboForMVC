class Update {

    /**
     * 表示フォーム
     */
    #displayForm = null;
    /**
     * 検索開始日デートボックス
     */
    #firstDate = null;
    /**
     * 検索終了日デートボックス
     */
    #lastDate = null;
    /**
     * 費目IDテキストボックス
     */
    #himokuId = null;
    /**
     * 明細セレクトボックス
     */
    #meisai = null;
    /**
     * 表示ボタン
     */
    #displayButton = null;

    /**
     * 更新フォーム
     */
    #updateForm = null;
    /**
     * 削除フォーム
     */
    #deleteForm = null;
    /**
     * 確認ダイアログ表示フラグ
     */
    #showDialog = null;
    /**
     * 削除ID
     */
    #deleteId = null;
    /**
     * 更新ボタン
     */
    #updeteButtons = null;
    /**
     * 削除ボタン
     */
    #deleteButtons = null;

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
        this.#displayForm = document.getElementById('displayForm')
        this.#firstDate = document.getElementById('firstDate');
        this.#lastDate = document.getElementById('lastDate');
        this.#himokuId = document.getElementById('himokuId');
        this.#meisai = document.getElementById('meisai');
        this.#displayButton = document.getElementById('displayButton')
        this.#updateForm = document.getElementById('updateForm');
        this.#deleteForm = document.getElementById('deleteForm');
        this.#showDialog = document.getElementById('showDialog');
        this.#deleteId = document.getElementById('deleteId');
        this.#updeteButtons = document.querySelectorAll('.updateButtons');
        this.#deleteButtons = document.querySelectorAll('.deleteButtons');
        this.#printTag = document.getElementById('printTag');
        this.#printbleArea = document.getElementById('printableArea');

        this.#displayButton.addEventListener('click', e => this.#displayButtonOnClick(e));
        this.#updeteButtons.forEach(x => x.addEventListener('click', e => this.#updateButtonOnClick(e)));
        this.#deleteButtons.forEach(x => x.addEventListener('click', e => this.#deleteButtonOnClick(e)));
        this.#printTag.addEventListener('click', e => this.#printTagOnClick(e));

        // 初期化の最終処理
        this.#windowOnLoad();
    }

    /**
     * 検索ボタンを押下してUpdateにGet
     * @param {Event} e
     */
    #displayButtonOnClick(e) {
        this.#displayForm.submit();
    }

    /**
     * 更新ボタンを押下してUpdateにPost
     * @param {Event} e
     */
    #updateButtonOnClick(e) {
        const updateId = e.target.dataset.id;
        const updateHiduke = document.getElementById(e.target.dataset.hidukeId).value;
        const updateHimokuId = document.getElementById(e.target.dataset.himokuidId).value;
        const updateMeisai = document.getElementById(e.target.dataset.meisaiId).value;
        const updateNyukinGaku = document.getElementById(e.target.dataset.nyukingakuId).value;
        const updateShukinGaku = document.getElementById(e.target.dataset.shukingakuId).value;

        this.#updateForm.action = '/Kakeibo/Update/?' +
            `FirstDate=${this.#firstDate.value}&` +
            `LastDate=${this.#lastDate.value}&` +
            `HimokuId=${this.#himokuId.value}&` +
            `Meisai=${this.#meisai.value}&` +

            `UpdateId=${updateId}&` +
            `UpdateHiduke=${updateHiduke}&` +
            `UpdateHimokuId=${updateHimokuId}&` +
            `UpdateMeisai=${updateMeisai}&` +
            `UpdateNyukinGaku=${updateNyukinGaku}&` +
            `UpdateShukinGaku=${updateShukinGaku}`;

        this.#updateForm.submit();
    }

    /**
     * 削除ボタンを押下してdeleteにPost
     * @param {Event} e
     */
    #deleteButtonOnClick(e) {
        this.#deleteForm.action = this.#deletFormUrl(e.target.dataset.id);
        this.#deleteForm.submit();
    }

    /**
     * 削除フォームのUrl
     * @param {any} deleteId
     * @returns
     */
    #deletFormUrl(deleteId) {
        return '/Kakeibo/Delete/?' +
            `FirstDate=${this.#firstDate.value}&` +
            `LastDate=${this.#lastDate.value}&` +
            `HimokuId=${this.#himokuId.value}&` +
            `Meisai=${this.#meisai.value}&` +

            `ShowDialog=${this.#showDialog.value}&` +
            `UpdateId=${deleteId}`;
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
    }

    /**
    * 初期化の最後処理
    */
    #windowOnLoad() {
        // 確認ダイアログ表示フラグがTrueの場合、確認ダイアログを表示
        if (this.#showDialog.value === 'True') {
            const result = window.confirm('削除してもよろしいですか？');
            // 確認ダイアログの結果によって処理を分岐
            if (result == true) {
                this.#deleteForm.action = this.#deletFormUrl(this.#deleteId.value);
                this.#deleteForm.submit();
            }
            else {
                this.#showDialog.value = 'False';
            }
        }
        // フォーカスを検索開始日デートボックスにセット
        this.#firstDate.focus();
    }
}

window.onload = function () {
    let obj = new Update;
    obj.init()
};