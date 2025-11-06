class Update {

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
        this.#firstDate = document.getElementById('firstDate');
        this.#lastDate = document.getElementById('lastDate');
        this.#himokuId = document.getElementById('himokuIdText');
        this.#meisai = document.getElementById('meisaiSelect');
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
        const firstDate = this.#firstDate.value;
        const lastDate = this.#lastDate.value;
        const himokuId = this.#himokuId.value;
        const meisai = this.#meisai.value;

        const updateId = e.target.dataset.id;
        const updateHiduke = document.getElementById(e.target.dataset.hidukeId).value;
        const updateHimokuId = document.getElementById(e.target.dataset.himokuidId).value;
        const updateMeisai = document.getElementById(e.target.dataset.meisaiId).value;
        const updateNyukinGaku = document.getElementById(e.target.dataset.nyukingakuId).value;
        const updateShukinGaku = document.getElementById(e.target.dataset.shukingakuId).value;

        const form = document.getElementById(e.target.dataset.formId);
        form.action = '/Kakeibo/Update/?' +
            `FirstDate=${firstDate}&` +
            `LastDate=${lastDate}&` +
            `HimokuId=${himokuId}&` +
            `Meisai=${meisai}&` +
            `UpdateId=${updateId}&` +
            `UpdateHiduke=${updateHiduke}&` +
            `UpdateHimokuId=${updateHimokuId}&` +
            `UpdateMeisa=${updateMeisai}&` +
            `UpdateNyukinGaku=${updateNyukinGaku}&` +
            `UpdateShukinGaku=${updateShukinGaku}`;
        form.submit();
    }

    /**
     * 削除ボタンを押下してdeleteにPost
     * @param {Event} e
     */
    #deleteButtonOnClick(e) {
        const firstDate = this.#firstDate.value;
        const lastDate = this.#lastDate.value;
        const himokuId = this.#himokuId.value;
        const meisai = this.#meisai.value;

        const deleteId = e.target.dataset.id;

        const form = document.getElementById(e.target.dataset.formId);
        form.action = '/Kakeibo/Delete/?' +
            `FirstDate=${firstDate}&` +
            `LastDate=${lastDate}&` +
            `HimokuId=${himokuId}&` +
            `Meisai=${meisai}&` +
            `UpdateId=${deleteId}&`;
        form.submit();
    }

    /**
    * 初期化の最後処理
    */
    #windowOnLoad() {
        this.#firstDate.focus();
    }
}

window.onload = function () {
    let obj = new Update;
    obj.init()
};