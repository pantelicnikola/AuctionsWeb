$.connection.hub.start()
    .done(function () {
        console.log("Connected to SignalR");
        $.connection.myHub.server.announce("KUUUUUUUURCINA");
    })
    .fail(function () { alert("Failed connection to SignalR"); });

$.connection.myHub.client.announce = function (announcement) {
    alert(announcement)
}