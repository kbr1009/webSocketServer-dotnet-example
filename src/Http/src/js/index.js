window.onload = function () {
}

// WebSocketサーバーのURL (例: "ws://localhost:8080")
var wsUrl = "ws://localhost:8080/ws";
// WebSocket接続を開く
var ws = new WebSocket(wsUrl);
ws.onopen = function () {
    console.log("接続が開かれました。");
};

// サーバーからのメッセージを受信したときの処理
ws.onmessage = function (event) {
    var messages = document.getElementById('messages');
    var message = document.createElement('p');
    if (event.data) {
        message.textContent = event.data;
        messages.appendChild(message);
    }
};

document.getElementById("inputMessage").addEventListener("change", function () {
    document.getElementById("sendMessage").disabled = false;
});

// ボタンがクリックされたときのイベントリスナー
document.getElementById('sendMessage').addEventListener('click', function () {
    var sendTxt = document.getElementById('inputMessage');
    if (!(sendTxt.value)) {
        alert('送信するメッセージを入力してください。');
    }
    else {
        ws.send(sendTxt.value);
    }
});

// WebSocketエラーの処理
ws.onerror = function (event) {
    console.error("WebSocketエラーが発生しました: ", event);
};

// WebSocket接続が閉じたときの処理
ws.onclose = function (event) {
    console.log("接続が閉じられました: ", event);
};