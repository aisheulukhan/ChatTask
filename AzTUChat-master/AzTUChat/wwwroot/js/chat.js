"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

ChangeUser()

connection.on("ReciveMessage", function (message, userName) {
    let id = "#" + userName;
    if ($(id).parent().parent()) {

        $(".discussion").removeClass("message-active");
        $(id).parent().parent().addClass("message-active");
    }
    let msg = `<div class="message">
                             <p class="text"> ${message}</p>
                        </div>`
    $(".messages-chat").append(msg);
});

connection.on("Connected", function (username) {
    let id = "#" + username;
    $(id).removeClass("offline")
    $(id).addClass("online")
})

connection.on("DisConnected", function (username) {
    let id = "#" + username;
    $(id).removeClass("online")
    $(id).addClass("offline")
})

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

$("#sendButton").click(function () {
    let message = $("#messageInput").val();
    let userName = $(this).attr("username");
    connection.invoke("SendMessage", userName, message).then(function () {
        $("#messageInput").val("");
    }).catch(function (err) {
        return console.error(err.toString());
    });
})

$(".discussion").not("search").click(function () {
    $(".discussion").removeClass("message-active");
    $(this).addClass("message-active");
    $("#sendButton").attr("username", $(this).find(".status").attr("id"))

})

function ChangeUser() {
    $(".discussion").eq(1).addClass("message-active");
    $("#sendButton").attr("username", $(".discussion .status").attr("id"))

}
