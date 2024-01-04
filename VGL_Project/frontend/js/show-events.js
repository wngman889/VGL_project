$(document).ready(function () {
    getAllEvents();
});

function getAllEvents() {
    $.get("https://localhost:7262/api/Events/get-all-events")
        .done(function (data) {
            var eventsContainer = $("#eventsContainer");
            eventsContainer.empty();

            $.each(data, function (index, event) {
                getGameDetails(event.gameId, function (gameDetails) {
                    getUserDetails(event.authorId, function (userDetails) {
                        var eventCard = $("<div>").addClass("latest-card");

                        // Left Section
                        var leftSection = $("<div>").addClass("card-left-section");
                        var imgContainer = $("<div>").addClass("latest-img-container");

                        if (event.EventImage && event.EventImage.length > 0) {
                            var img = $("<img>").addClass("latest-image").attr("src", "data:image/png;base64," + event.EventImage).attr("alt", "Event Image");
                            imgContainer.append(img);
                        }

                        var overlay = $("<div>").addClass("latest-overlay");
                        imgContainer.append(overlay);

                        var gameInfo = $("<div>").addClass("latest-game-info");
                        var starRating = $("<div>").addClass("game-rating");

                        for (var i = 0; i < event.rating; i++) {
                            starRating.append("<img class='game-rating-star' src='imgs/misc/reviewer-full-star.png' alt='Star'>");
                        }

                        var gameName = $("<div>").addClass("latest-game-name").text(gameDetails ? gameDetails.title : "N/A");

                        gameInfo.append(starRating).append(gameName);
                        leftSection.append(imgContainer).append(gameInfo);

                        // Right Section
                        var rightSection = $("<div>").addClass("card-right-section");
                        var reviewContainer = $("<div>").addClass("latest-review-container");
                        var review = $("<div>").addClass("latest-review").text(event.description);
                        var settingsIcon = $("<img>").addClass("latest-settings").attr("src", "imgs/misc/icon-settings.png").attr("alt", "Settings");

                        reviewContainer.append(review).append(settingsIcon);

                        var reviewInfo = $("<div>").addClass("review-info");
                        var reviewName = $("<div>").addClass("review-name").text(userDetails ? userDetails.username : "N/A");
                        var reviewTime = $("<div>").addClass("review-time").text("Time: " + event.date);

                        reviewInfo.append(reviewName).append(reviewTime);
                        rightSection.append(reviewContainer).append(reviewInfo);

                        eventCard.append(leftSection).append(rightSection);

                        eventsContainer.append(eventCard);
                    });
                });
            });
        })
        .fail(function (error) {
            alert("Error getting all events: " + error.responseText);
        });
}

function getGameDetails(gameId, callback) {
    $.get("https://localhost:7262/api/Games/get-game-by-id?id=" + gameId)
        .done(function (data) {
            callback(data);
        })
        .fail(function (error) {
            console.error("Error getting game details: " + error.responseText);
            callback(null);
        });
}

function getUserDetails(userId, callback) {
    $.get("https://localhost:7262/api/User/get-user-by-id?id=" + userId)
        .done(function (data) {
            callback(data);
        })
        .fail(function (error) {
            console.error("Error getting user details: " + error.responseText);
            callback(null);
        });
}
