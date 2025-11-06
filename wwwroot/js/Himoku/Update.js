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
        this.#inputForm = document.getElementById('inputForm');
        this.#nameText = document.getElementById('nameText');
        this.#inputButton = document.getElementById('inputButton');
        this.#updateForm = document.getElementById('updateForm');
        this.#deleteForm = document.getElementById('deleteForm');
        this.#updeteButtons = document.querySelectorAll('.updateButtons');
        this.#deleteButtons = document.querySelectorAll('.deleteButtons');

        this.#inputButton.addEventListener('click', e => this.#inputButtonOnClick(e));
        this.#updeteButtons.forEach(x => x.addEventListener('click', e => this.#updateButtonOnClick(e)));
        this.#deleteButtons.forEach(x => x.addEventListener('click', e => this.#deleteButtonOnClick(e)));

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
     * 編集ボタンを押下してUpdateにPost
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
        const deleteId = e.target.dataset.id;

        this.#deleteForm.action = '/Himoku/Delete/?' +
            `Name=${this.#nameText.value}&` +
            `UpdateId=${deleteId}`;
        this.#deleteForm.submit();
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