class Update {

    /**
     * 入力フォーム
     */
    #inputForm = null;
    /**
     * 検索開始日デートボックス
     */
    #nameText = null;
    /**
     * 登録ボタン
     */
    #inputButton = null;

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
        this.#inputForm = document.getElementById('inputForm');
        this.#nameText = document.getElementById('nameText');
        this.#inputButton = document.getElementById('inputButton');
        this.#updateForm = document.getElementById('updateForm');
        this.#deleteForm = document.getElementById('deleteForm');
        this.#showDialog = document.getElementById('showDialog');
        this.#deleteId = document.getElementById('deleteId');
        this.#updeteButtons = document.querySelectorAll('.updateButtons');
        this.#deleteButtons = document.querySelectorAll('.deleteButtons');
        this.#printTag = document.getElementById('printTag');
        this.#printbleArea = document.getElementById('printableArea');

        this.#inputButton.addEventListener('click', e => this.#inputButtonOnClick(e));
        this.#updeteButtons.forEach(x => x.addEventListener('click', e => this.#updateButtonOnClick(e)));
        this.#deleteButtons.forEach(x => x.addEventListener('click', e => this.#deleteButtonOnClick(e)));
        this.#printTag.addEventListener('click', e => this.#printTagOnClick(e));


        // 初期化の最終処理
        this.#windowOnLoad();
    }

    /**
     * 登録ボタンを押下してInputにPost
     * @param {Event} e
     */
    #inputButtonOnClick(e) {
        this.#inputForm.action = '/Himoku/Input/?' +
            `Name=${this.#nameText.value}`;
        this.#inputForm.submit();
    }

    /**
     * 更新ボタンを押下してUpdateにPost
     * @param {Event} e
     */
    #updateButtonOnClick(e) {
        const updateId = e.target.dataset.id;
        const updateName = document.getElementById(e.target.dataset.nameId).value;

        this.#updateForm.action = '/Himoku/Update/?' +
            `Name=${this.#nameText.value}&` +
            `UpdateId=${updateId}&` +
            `UpdateName=${updateName}`;
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
        return '/Himoku/Delete/?' +
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
        location.reload();
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
        this.#nameText.focus();
    }
}

window.onload = function () {
    let obj = new Update;
    obj.init()
};