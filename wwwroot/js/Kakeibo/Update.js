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
     * 費目IDセレクトボックス
     */
    #himokuId = null;
    /**
     * 明細テキストボックス
     */
    #meisai = null;
    /**
     * 明細リスト
     */
    #meisaiList = null;
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
     * プリントタグ
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
     * 確認ダイアログ
     */
    #comfirmDialog = null;

    /**
     * ＯＫボタン
     */
    #okButton = null;
    /**
     * キャンセルボタン
     */
    #cancelButton = null;

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
        this.#meisaiList = document.getElementById('meisaiList');
        this.#displayButton = document.getElementById('displayButton')
        this.#updateForm = document.getElementById('updateForm');
        this.#deleteForm = document.getElementById('deleteForm');
        this.#showDialog = document.getElementById('showDialog');
        this.#deleteId = document.getElementById('deleteId');
        this.#updeteButtons = document.querySelectorAll('.updateButtons');
        this.#deleteButtons = document.querySelectorAll('.deleteButtons');
        this.#print = document.getElementById('print');
        this.#printbleArea = document.getElementById('printableArea');
        this.#tableArea = document.getElementById('table-area');
        this.#copy = document.getElementById('copy');
        this.#table = document.getElementById('table');
        this.#comfirmDialog = document.getElementById('comfirmDialog');
        this.#okButton = document.getElementById('okButton');
        this.#cancelButton = document.getElementById('cancelButton')

        this.#himokuId.addEventListener('change', e => this.#himokuIdOnChange(e));
        this.#displayButton.addEventListener('click', e => this.#displayButtonOnClick(e));
        this.#updeteButtons.forEach(x => x.addEventListener('click', e => this.#updateButtonOnClick(e)));
        this.#deleteButtons.forEach(x => x.addEventListener('click', e => this.#deleteButtonOnClick(e)));
        this.#print.addEventListener('click', e => this.#printOnClick(e));
        this.#copy.addEventListener('click', e => this.#copyOnClick(e));
        this.#okButton.addEventListener('click', e => this.#okButtonOnClick(e));
        this.#cancelButton.addEventListener('click', e => this.#cancelButtonOnClick(e));

        // 初期化の最終処理
        this.#windowOnLoad();
    }

    /**
     * 費目ID変更
     * @param {Event} e
     */
    #himokuIdOnChange(e) {
        // 明細履歴リスト取得
        this.#getMeisaiList(e.target.value);
    }

    /**
     * 検索ボタンを押下
     * @param {Event} e
     */
    #displayButtonOnClick(e) {
        this.#displayForm.submit();
    }

    /**
     * 更新ボタンを押下
     * @param {Event} e
     */
    #updateButtonOnClick(e) {
        const dataset = e.target.dataset;
        const updateId = dataset.id;
        const updateHiduke = document.getElementById(dataset.hidukeId).value;
        const updateHimokuId = document.getElementById(dataset.himokuidId).value;
        const updateMeisai = document.getElementById(dataset.meisaiId).value;
        const updateNyukinGaku = document.getElementById(dataset.nyukingakuId).value;
        const updateShukinGaku = document.getElementById(dataset.shukingakuId).value;

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
     * 削除ボタンを押下
     * @param {Event} e
     */
    #deleteButtonOnClick(e) {
        this.#deleteForm.action = this.#deletFormUrl(e.target.dataset.id);
        this.#deleteForm.submit();
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
            alert(`クリップボードへのコピーに失敗しました。: ${error}`);
        }
    }

    /**
     * 確認ダイアログのはいボタン押下
     * @param {Event} e
     */
    #okButtonOnClick(e) {
        this.#deleteForm.action = this.#deletFormUrl(this.#deleteId.value);
        this.#deleteForm.submit();
        this.#comfirmDialog.close('ok');
    }

    /**
     * 確認ダイアログのキャンセルボタン押下
     * @param {Event} e
     */
    #cancelButtonOnClick(e) {
        this.#showDialog.value = 'False';
        this.#comfirmDialog.close('cancel');
        // 初期表示フォーカス設定
        this.#firstDate.focus();
    }

    /**
     * 明細履歴リスト取得
     * @param {String} himokuIdValue
     */
    async #getMeisaiList(himokuIdValue) {
        try {
            const response = await fetch('/Kakeibo/GetMeisaiList/', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ himokuId: himokuIdValue, }),
            });

            if (!response.ok) {
                throw new Error(`ネットワークレスポンスが不正です。status: ${response.status}`);
            }

            const responseData = await response.json();

            this.#meisaiList.innerHTML = "";
            for (const item of responseData.meisaiList) {
                const option = document.createElement('option');
                option.value = item;
                this.#meisaiList.appendChild(option);
            };
        } catch (error) {
            alert(`明細履歴リストの取得に失敗しました。: ${error}`);
        }
    }

    /**
     * 削除フォームのアクションUrl
     * @param {String} deleteIdValue
     * @returns
     */
    #deletFormUrl(deleteIdValue) {
        return '/Kakeibo/Delete/?' +
            `FirstDate=${this.#firstDate.value}&` +
            `LastDate=${this.#lastDate.value}&` +
            `HimokuId=${this.#himokuId.value}&` +
            `Meisai=${this.#meisai.value}&` +

            `ShowDialog=${this.#showDialog.value}&` +
            `UpdateId=${deleteIdValue}`;
    }

    /**
    * 初期化の最後処理
    */
    #windowOnLoad() {
        // 確認ダイアログ表示フラグがTrueの場合、確認ダイアログを表示
        if (this.#showDialog.value === 'True') {
            // 確認ダイアログの結果によって処理を分岐
            // ＯＫボタン→#okButtonOnCrick
            // キャンセルボタン→#cancelButtonOnClick
            this.#comfirmDialog.showModal();
        } else {
            // 初期表示フォーカス設定
            this.#firstDate.focus();
        }
    }
}

window.onload = function () {
    let obj = new Update;
    obj.init()
};