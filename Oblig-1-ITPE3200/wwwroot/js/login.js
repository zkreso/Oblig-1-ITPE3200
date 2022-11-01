function logIn() {
    const usernameOk = validateUsername($("#username").val());
    const passwordOk = validatePassword($("#password").val());

    if (usernameOk && passwordOk) {
        const user = {
            username: $("#username").val(),
            password: $("#password").val()
        }
        $.post("oblig/LogIn", user, function (OK) {
            if (OK) {
                window.location.href = 'index.html';
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

