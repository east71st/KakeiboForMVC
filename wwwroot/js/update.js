class update {

    /**
     * 検索開始期間テキストボックス
     */
    #firstDateText = null;

    init() {
        this.#firstDateText = document.getElementById('firstDateText');

        document.querySelectorAll('.update').forEach(x => x.addEventListener('click', e => this.#updateButtonOnClick(e)));
        this.#firstDateText.focus();
    }

    /**
     * 編集ボタンを押下してUpdateにPost
     * @param {any} e
     */
    #updateButtonOnClick(e) {
        document.updateForm.submit();
    }

}

window.onload = function () {
    let obj = new update;
    obj.init()
};