//Search email addresses in event class and add mailto link
function ParseEmailAddresses() {
    var regEx = /(\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)/;

    $(".event").filter(function () {
        return $(this).html().match(regEx);
    }).each(function () {
        $(this).html($(this).html().replace(regEx, "<a href=\"mailto:$1\">$1</a>"));
    });
}

function GetMediaStatus() {
    $.get("/Medien/Status/").done(function (data) {
      $('#BookCount').html(data.BookCount);
      $('#MovieCount').html(data.MovieCount);
      $('#AudioBookCount').html(data.AudioBookCount);
  });
}