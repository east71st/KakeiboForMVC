class Update {

    /**
     * 検索開始日デートボックス
     */
    #nameText = null;

    /**
     * 更新ボタン
     */
    #updeteButtons = null;

    /**
     * 削除ボタン
     */
    #deleteButtons = null;

    /**
     * コンストラクタ
     */
    constructor() { };

    /**
     * 初期化
     */
    init() {
        this.#nameText = document.getElementById('nameText');
        this.#updeteButtons = document.querySelectorAll('.update');
        this.#deleteButtons = document.querySelectorAll('.delete');

        this.#updeteButtons.forEach(x => x.addEventListener('click', e => this.#updateButtonOnClick(e)));
        this.#deleteButtons.forEach(x => x.addEventListener('click', e => this.#deleteButtonOnClick(e)));

        // 初期化の最終処理
        this.#windowOnLoad();
    }

    /**
     * 編集ボタンを押下してUpdateにPost
     * @param {Event} e
     */
    #updateButtonOnClick(e) {
        const name = this.#nameText.value;

        const updateId = e.target.dataset.id;
        const updateName = document.getElementById(e.target.dataset.nameId).value;

        const form = document.getElementById(e.target.dataset.formId);
        form.action = '/Himoku/Update/?' +
            `Name=${name}&` +
            `UpdateId=${updateId}&` +
            `UpdateName=${updateName}&`;
        form.submit();
    }

    /**
     * 削除ボタンを押下してdeleteにPost
     * @param {Event} e
     */
    #deleteButtonOnClick(e) {
        const name = this.#nameText.value;

        const deleteId = e.target.dataset.id;

        const form = document.getElementById(e.target.dataset.formId);
        form.action = '/Himoku/Delete/?' +
            `Name=${name}&` +
            `UpdateId=${deleteId}&`;
        form.submit();
    }

    /**
    * 初期化の最後処理
    */
    #windowOnLoad() {
        this.#nameText.focus();
    }
}

window.onload = function () {
    let obj = new Update;
    obj.init()
};