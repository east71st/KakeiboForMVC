class Display {

    /**
     * 送信フォーム
     */
    #tempDataForm = null;
    /**
     * 検索開始日デートボックス
     */
    #firstDate = null;
    /**
     * 費目IDセレクトボックス
     */
    #himokuId = null;
    /**
     * 明細リスト
     */
    #meisaiList = null;
    /**
     * セッションストレージに保管する入力ボックス
     */
    #tempElements = null;
    /**
     * ゲットからのレスポンスか否か
     */
    #isGet = null;

    /**
     * プリント
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
     * CSVダウンロード
     */
    #csvDownload = null;

    /**
     * セッションストレージに保管するデータのキー名
     */
    #tempDataName = this.constructor.name + 'TempData';

    /**
     * コンストラクタ
     */
    constructor() { };

    /**
     * 初期化
     */
    init() {
        this.#tempDataForm = document.getElementById('tempDataForm');
        this.#firstDate = document.getElementById('firstDate');
        this.#tempElements = document.querySelectorAll('.tempElements');
        this.#isGet = document.getElementById('isGet');
        this.#himokuId = document.getElementById('himokuId');
        this.#meisaiList = document.getElementById('meisaiList');
        this.#print = document.getElementById('print');
        this.#printbleArea = document.getElementById('printableArea');
        this.#tableArea = document.getElementById('table-area');
        this.#copy = document.getElementById('copy');
        this.#csvDownload = document.getElementById('csvDownload');
        this.#table = document.getElementById('table');

        this.#tempDataForm.addEventListener('submit', e => this.#formOnSubmit(e));
        this.#himokuId.addEventListener('change', e => this.#himokuIdOnChange(e));
        this.#print.addEventListener('click', e => this.#printOnClick(e));
        this.#copy.addEventListener('click', e => this.#copyOnClick(e));
        this.#csvDownload.addEventListener('click', e => this.#csvDownloadOnClick(e));

        // 初期化の最終処理
        this.#windowOnLoad();
    }

    /**
     * データが送信されたときの処理
     * @param {event} e
     */
    #formOnSubmit(e) {
        // 入力データをセッションストレージに保管
        this.#setTempData();
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
            alert(`クリップボードへのコピーに失敗しました。:${error}`);
        }
    }

    /**
     * CSVダウンロード
     * @param {Event} e
     */
    async #csvDownloadOnClick(e) {
        let body = null;
        if (sessionStorage.getItem(this.#tempDataName) != null) {
            let map = new Map(
                Object.entries(JSON.parse(sessionStorage.getItem(this.#tempDataName))));
            body = {
                firstDate: map.get("FirstDate"),
                lastDate: map.get("LastDate"),
                himokuId: map.get("HimokuId"),
                meisai: map.get("Meisai"),
            };
        }

        try {
            const response = await fetch('/Kakeibo/DownloadCsv/', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(body),
            });

            if (!response.ok) {
                throw new Error(`ネットワークレスポンスが不正です。status: ${response.status}`);
            }

            let filename = 'download.csv';

            const contentDisposition = response.headers.get('Content-Disposition');
            if (contentDisposition && contentDisposition.indexOf('attachment') !== -1) {
                const filenameStarMatch = /filename\*=UTF-8''(.+)/.exec(contentDisposition);
                if (filenameStarMatch && filenameStarMatch[1]) {
                    filename = decodeURIComponent(filenameStarMatch[1]);
                }
            }

            const csvData = await response.text();
            const bom = new Uint8Array([0xEF, 0xBB, 0xBF]);
            const blob = new Blob([bom, csvData], { type: 'text/csv;charset=utf-8;' });

            const link = document.createElement('a');
            link.style.display = 'none';
            link.href = window.URL.createObjectURL(blob);
            link.download = filename;

            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);

        } catch (error) {
            alert(`CSVファイルのダウンロードに失敗しました。: ${error}`);
        }
    }

    /**
     * 入力データをセッションストレージに保管
     */
    #setTempData() {
        const map = new Map();
        this.#tempElements.forEach(x => map.set(x.name, x.value));
        sessionStorage.setItem(
            this.#tempDataName, JSON.stringify(Object.fromEntries(map)));
    }

    /**
     * セッションストレージにデータが保管されている場合はデータを取得して入力ボックスに設定
     * データが保管されていない場合は入力ボックスからデータを取得して、ストレージに保管
     */
    #getTempData() {
        if (sessionStorage.getItem(this.#tempDataName) != null) {
            const map = new Map(
                Object.entries(JSON.parse(sessionStorage.getItem(this.#tempDataName))));
            this.#tempElements.forEach(x => x.value = map.get(x.name));
        } else {
            this.#setTempData();
        }
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

            const x = responseData.meisaiList;
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
    * 初期化の最後処理
    */
    #windowOnLoad() {
        // ゲットからのレスポンスでセッションストレージにデータが保管せれている場合は再表示
        if (this.#isGet.value === 'True') {
            // セッションストレージにデータが保管されている場合はデータを取得して入力ボックスに設定
            // データが保管されていない場合は入力ボックスからデータを取得して、ストレージに保管
            this.#getTempData();
            this.#tempDataForm.submit();
        }
        // 初期表示フォーカス設定
        this.#firstDate.focus();
    }
}

window.onload = function () {
    let obj = new Display;
    obj.init()
};