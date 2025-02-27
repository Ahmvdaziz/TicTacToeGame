// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function playMove(cellIndex) {
    $.ajax({
        url: '/Game/Play',
        type: 'POST',
        contentType: 'application/json', // تأكد إن النوع JSON
        data: JSON.stringify({ cellIndex: cellIndex }),
        success: function (data) {
            $("#game-board").html(data); // تحديث اللعبة
        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}
