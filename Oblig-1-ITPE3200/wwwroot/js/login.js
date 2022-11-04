/*$(function () {
    $.post("oblig/IsLoggedIn", function (OK) {
        if (OK) {
            window.location.href = "admin.html";
        }
    });
});*/

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
                window.location.href = 'diseaselist.html';
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

function validateUsername(username) {
    const regExp = /^[a-zA-ZæøåÆØÅ\.\ ]{2,20}$/;
    const ok = regExp.test(username);

    if (ok) {
        $("#errUser").html("");
        return true;
    }
    else {
        $("#errUser").html("Username must be 2 to 20 characters long.");
        return false;
    }
}

function validatePassword(password) {
    const regExp = /^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$/;
    const ok = regExp.test(password);

    if (ok) {
        $("#errPass").html("");
        return true;
    }
    else {
        $("#errPass").html("Password must be at least 8 characters long, and have one capital letter and one number");
        return false;
    }
}

