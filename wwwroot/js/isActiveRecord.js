const myId = document.getElementById("myId");
let inactivityTimeout;
let wasInactive = true;

const connectionRecord = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7271/isactive")
    .configureLogging(signalR.LogLevel.Information)
    .build();

connectionRecord.start()
.then(e => {
    
})
.catch(u => console.error(u.toString()));


function onUserInactive() {
  console.log("Пользователь неактивен более 30 секунд");
  if (connectionRecord.state === signalR.HubConnectionState.Connected) {
    connectionRecord.invoke("ChangeStatus", myId.value, "false").catch(e => console.error(e.toString()));
  }
  wasInactive = true;
}

function onUserActive() {
  if (wasInactive) {
    console.log("Пользователь активен");
    wasInactive = false;
    if (connectionRecord.state === signalR.HubConnectionState.Connected) {
      connectionRecord.invoke("ChangeStatus", myId.value, "true").catch(e => console.error(e.toString()));
    }
  }
}

// Функция для сброса таймера
function resetInactivityTimeout() {
  clearTimeout(inactivityTimeout);
  inactivityTimeout = setTimeout(() => {
    onUserInactive();
  }, 30000); // 20 секунд
  onUserActive(); // каждый раз при событии - считаем, что пользователь активен
}

// Список событий
const events = ['mousemove', 'keydown', 'scroll', 'click', 'touchstart'];

// Повесим обработчики
    events.forEach(event => {
    window.addEventListener(event, resetInactivityTimeout);
});