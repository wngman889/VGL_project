$(document).ready(function () {
    getAllEvents();
});

function getAllEvents() {
    $.get("https://localhost:7262/api/Events/get-all-events")
        .done(function (data) {
            var eventsList = $("#eventsList");
            eventsList.empty();

            $.each(data, function (index, event) {
                // Fetch game and user details for each event
                getGameDetails(event.gameId, function (gameDetails) {
                    getUserDetails(event.authorId, function (userDetails) {
                        var gameTitle = gameDetails ? gameDetails.title : "N/A";
                        var userName = userDetails ? userDetails.username : "N/A";

                        eventsList.append("<li>" + event.description + " - " + event.date +
                            " - Game: " + gameTitle + " - Author: " + userName + "</li>");
                    });
                });
            });
        })
        .fail(function (error) {
            console.error("Error getting all events: " + error.responseText);
        });
}

function getGameDetails(gameId, callback) {
    $.get("https://localhost:7262/api/Games/get-game?id=" + gameId)
        .done(function (data) {
            callback(data);
        })
        .fail(function (error) {
            console.error("Error getting game details: " + error.responseText);
            callback(null); // Pass null if there is an error
        });
}

function getUserDetails(userId, callback) {
    $.get("https://localhost:7262/api/Users/get-user?id=" + userId)
        .done(function (data) {
            callback(data);
        })
        .fail(function (error) {
            console.error("Error getting user details: " + error.responseText);
            callback(null); // Pass null if there is an error
        });
}
