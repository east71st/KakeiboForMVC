class KeyValue {
    static Esc = "Escape";
}

class MessageBoxResult {
    static Ok = "ok";
    static Cancel = "cancel";
    static Yes = "yes";
    static No = "no";
    static OkCancel = [MessageBoxResult.Ok, MessageBoxResult.Cancel];
    static YesNo = [MessageBoxResult.Yes, MessageBoxResult.No];
}

class MessageBoxIcon {
    static Information = "information";
    static Exclamation = "exclamation";
    static Question = "question";
}

class MessageBox {
    static async Info(message, title = "情報") {
        return new MessageBoxElement().Show(message, title, MessageBoxIcon.Information, MessageBoxResult.Ok);
    }

    static async Alert(message, title = "エラー") {
        return new MessageBoxElement().Show(message, title, MessageBoxIcon.Exclamation, MessageBoxResult.Ok);
    }

    static async Confirm(message, title = "確認") {
        return new MessageBoxElement().Show(message, title, MessageBoxIcon.Question, MessageBoxResult.OkCancel);
    }
}

class MessageBoxElement extends HTMLElement {
    constructor() {
        super();
        this.Body = document.createElement("div");
        this.Title = document.createElement("div");
        this.Message = document.createElement("div");
        this.ButtonContainer = document.createElement("div");
        this.OkButton = null;
        this.CancelButton = null;
        this.OldActiveElement = null;

        this.Build();
    }

    Build() {
        this.Body.classList.add("body");
        this.Title.classList.add("title");
        this.Message.classList.add("message");
        this.ButtonContainer.classList.add("button-container");

        this.Body.append(this.Title, this.Message, this.ButtonContainer);
        this.append(this.Body);

        this.addEventListener("keydown", this.OnKeyDown.bind(this));
    }

    Show(message, title, icon, buttonTypes) {
        if (!Array.isArray(buttonTypes)) buttonTypes = [buttonTypes];

        this.classList.add(icon);
        this.OldActiveElement = document.activeElement;

        this.Title.textContent = title;
        this.Message.textContent = message;

        return new Promise(resolve => {
            for (let type of buttonTypes) {
                let button = document.createElement("button");
                let caption;

                switch (type) {
                    case MessageBoxResult.Ok: caption = "OK"; break;
                    case MessageBoxResult.Cancel: caption = "キャンセル"; break;
                    case MessageBoxResult.Yes: caption = "はい"; break;
                    case MessageBoxResult.No: caption = "いいえ"; break;
                    default: throw new Error("Invalid ButtonType: " + type);
                }

                button.classList.add(type, "flat");
                button.textContent = caption;

                button.addEventListener("click", () => {
                    this.Close();
                    resolve(type);
                });

                this.ButtonContainer.append(button);

                switch (type) {
                    case MessageBoxResult.Ok:
                    case MessageBoxResult.Yes:
                        this.OkButton = button;
                        break;
                    case MessageBoxResult.Cancel:
                    case MessageBoxResult.No:
                        this.CancelButton = button;
                        break;
                }
            }
        });

        document.body.insertAdjacentElement("beforeend", this);
        this.SetDefaultFocus();
    }

    SetDefaultFocus() {
        if (this.CancelButton !== null)
            this.CancelButton.focus();
        else if (this.OkButton !== null)
            this.OkButton.focus();
        else
            this.focus();
    }

    Close() {
        this.remove();
        if (this.OldActiveElement) this.OldActiveElement.focus();
        document.body.classList.remove("message-box-shown");
    }

    OnKeyDown(e) {
        if (e.key === KeyValue.Esc) {
            e.stopPropagation();
            e.preventDefault();

            if (this.CancelButton !== null) {
                this.CancelButton.focus();
                this.CancelButton.click();
            } else if (this.OkButton !== null) {
                this.OkButton.focus();
                this.OkButton.click();
            }
        }
    }
}

customElements.define("message-box", MessageBoxElement);
