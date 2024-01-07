function addEvent() {
    var description = $("#description").val();
    var date = $("#date").val();
    var gameId = $("#gameId").val();
    var authorId = $("#authorId").val();

    var url = `https://localhost:7262/api/Events/add-event?description=${encodeURIComponent(description)}&date=${encodeURIComponent(date)}&gameId=${encodeURIComponent(gameId)}&authorId=${encodeURIComponent(authorId)}`;

    $.ajax({
        type: "POST",
        url: url,
        dataType: 'json',
        async: false,
        success: function (data) {
            alert(data);
        },
        error: function (error) {
            alert("Error adding event: " + error.responseText);
        }
    });
}