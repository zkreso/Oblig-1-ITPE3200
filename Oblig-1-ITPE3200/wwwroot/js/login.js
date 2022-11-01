function logIn() {
    const usernameOk = validateUsername($("#username").val());
    const passwordOk = validatePassword($("#password").val());

    if (usernameOk && passwordOk) {
        const user = {
            username: $("#username").val(),
            password: $("#password").val()
        };

        $.post("oblig/LogIn", user, function (OK) {
            if (OK) {
                changeSite();
            }
            else {
                $("#err").html("Wrong in username and password.");
            }
        })
        .fail(function () {
            $("#err").html("Something wrong accured on server. Try again later.");
        });
    }
}

function changeSite() {
    window.location.href = 'admin.html';
}

