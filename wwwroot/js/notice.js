if (!window.mySignalRHub) {
    window.mySignalRHub = new signalR.HubConnectionBuilder()
        .withUrl("/chat") // ваш URL хаба
        .configureLogging(signalR.LogLevel.Information)
        .build();

    // Запуск соединения
    window.mySignalRHub.start()
        .then(() => {
            console.log("SignalR подключен глобально");
        })
        .catch(err => {
            console.error("Ошибка при подключении к SignalR:", err);
        });
}



// Теперь во всех местах скрипта используйте window.mySignalRHub для работы:
function sendMessage(msg) {
    window.mySignalRHub.invoke("SendMessage", msg).catch(err => console.error(err.toString()));
}

// Для подписки
window.mySignalRHub.on("ReceiveMessage", message => {

});