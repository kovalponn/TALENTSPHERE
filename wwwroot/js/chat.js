"use strict";

const messageBlock = document.getElementById("messageList");

const sentMessage = '';
const receivedMessage = '';

const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7271/chat")
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.start()
.then(e => {
    const chatId = document.getElementById("inputChat").value;
    const userId = document.getElementById("inputUser").value;
    const userLogin = document.getElementById("inputLogin").value;
    connection.invoke("JoinToChat", chatId, userId, userLogin).catch(e => console.error(e.toString()));
    document.getElementById("join").style.display = 'none';
    document.getElementById("messages").style.display = 'block';
    // e.preventDefault();
})
.catch(u => console.error(u.toString()));

document.getElementById("joinButton")
.addEventListener("click", e =>  {
    const chatId = document.getElementById("inputChat").value;
    const userId = document.getElementById("inputUser").value;
    const userLogin = document.getElementById("inputLogin").value;
    connection.invoke("JoinToChat", chatId, userId, userLogin).catch(e => console.error(e.toString()));
    document.getElementById("join").style.display = 'none';
    document.getElementById("messages").style.display = 'block';
    // e.preventDefault();
});

document.getElementById("sendButton")
.addEventListener("click", e =>  {
    const chatId = document.getElementById("inputChat").value;
    const userId = document.getElementById("inputUser").value;
    const userLogin = document.getElementById("inputLogin").value;
    const message = document.getElementById("inputMessage").value;
    const companionId = document.getElementById("companion");
    const date = new Date();
    const isoString = date.toISOString();
    connection.invoke("SendMessage", chatId, userId, message, userLogin, isoString).catch(e => console.error(e.toString()));
    e.preventDefault();
});

connection.on("ReceiveMessage", (message, userId, userLogin, time, email) => {
    // const myId = document.getElementById("inputUser").value;
    // if (userId != myId) 
    // {
    //     companionId = userId;
    // }
    
    const templatesContainer = document.getElementById('templatesContainer');

    const inputEmail = document.getElementById("inputEmail").value;
    
    let template;

    if (inputEmail == email) {
        template = document.getElementById('myMessageExample');
    } else {
        template = document.getElementById('otherMessageExample');
    }

    if (templatesContainer && template) {

        const messageClone = template.cloneNode(true);
    
        messageClone.removeAttribute('id');
    

        const txtBody = messageClone.querySelector('#txtBody');
        if (txtBody) {
            txtBody.textContent = `${message}`;
        }

        const nameBody = messageClone.querySelector('#nameBody');
        if (nameBody) {
            nameBody.textContent = `${userLogin}`;
        }

        const timeBody = messageClone.querySelector('#time');
        if (timeBody) {
            timeBody.textContent = `${time}`;
        }


        const messageList = document.getElementById('messageList');
        messageList.appendChild(messageClone);
    } else {
        console.error('Шаблон или контейнер шаблонов не найдены.');
    }
});

connection.on("ReceiveNotice", (senderUserLogin, message) => {
    console.log(`Получено сообщение от ${senderUserLogin}: ${message}`);
    alert(`Новое уведомление \n ${senderUserLogin}: \n ${message}`);
});