const sampleActive = document.getElementById("sampleActive");
const sampleInactive = document.getElementById("sampleInactive");
const place = document.getElementById("placeForStatus");
const idTrack = document.getElementById("idTrack");

const connectionTrack = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7271/isactive")
    .configureLogging(signalR.LogLevel.Information)
    .build();

connectionTrack.start()
.then(e => {
    connectionTrack.invoke("TrackStatus", idTrack.value).catch(e => console.error(e.toString()));
})
.catch(u => console.error(u.toString()));

connectionTrack.on("ReceiveStatus", (statusStr) => {
    let isactive = false;

    if (statusStr == "true") {
        isactive = true;
    }

    if (isactive === true) {
    // Удаляем все дочерние элементы из place
    while (place.firstChild) {
        place.removeChild(place.firstChild);
    }
    // Вставляем sampleActive
    console.log("Вставляем sampleActive");
    place.appendChild(sampleActive);
    } else {
    // Удаляем все дочерние элементы из place
    while (place.firstChild) {
        place.removeChild(place.firstChild);
    }
    // Вставляем sampleInactive
    console.log("Вставляем sampleInactive");
    place.appendChild(sampleInactive);
}
});