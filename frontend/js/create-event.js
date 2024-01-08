function addEvent() {
    var description = $("#description").val();
    var date = $("#date").val();
    var gameId = $("#gameId").val();
    var authorId = $("#authorId").val();
    var eventImage = $("#eventImage")[0].files[0];

    var formData = new FormData();
    formData.append('description', description);
    formData.append('date', date);
    formData.append('gameId', gameId);
    formData.append('authorId', authorId);
    formData.append('eventImage', eventImage);

    var url = `https://localhost:7262/api/Events/add-event?description=${encodeURIComponent(description)}&date=${encodeURIComponent(date)}&gameId=${encodeURIComponent(gameId)}&authorId=${encodeURIComponent(authorId)}&eventImage=${encodeURIComponent(eventImage)}`;

    $.ajax({
        type: "POST",
        url: url,
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            alert(data);
        },
        error: function (error) {
            alert("Error adding event: " + error.responseText);
        }
    });
}

