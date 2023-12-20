function SubtmitLoading() {
    Ladda.bind('#btnSignin', //.btn-ladda-progress',
        {
            callback: function (instance) {
                callback(instance);
            }
        });
}

function onComplete(data) {

    var res = data.responseJSON;

    if (res.result == 0) {
        document.location.href = "../Home/Index";
    }
    else {
        NotificationUi.CrearNotificationBoxFor($("#dynamic-notificacion"), "alert-danger", res.content.mensaje, false);
        $("#btnSignin").removeAttr("disabled", "disabled");
    }
}


function callback(instance) {
    console.log("Ingresó a la funcion callbackk del login");
    //var progress = 0;
    var interval = setInterval(function () {

        $("form#SigninForm :input").each(function () {
            var input = $(this);
            if ($(input).attr('aria-invalid') === 'undefined' || $(input).attr('aria-invalid') === 'true') {
                instance.stop();
                clearInterval(interval);
                return false;
            }
        });

    }, 1000);
}

$(document).ready(function () {
    SubtmitLoading();
});