// Prints all diseases in table from start
$(function () {
    getAllDiseases();
})

// Gets all diseases from db
function getAllDiseases() {
    $.get("oblig/GetAllDiseases", function (diseases) {
        formatDiseases(diseases);
    });
}

// Formats diseases into html
function formatDiseases(diseases) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Name</th><th></th><th></th>" +
        "</tr>";
    for (let d of diseases) {
        ut += "<tr>" +
            "<td>" + d.name + "</td>" +
            "<td> <a class='btn btn-primary' href='change.html?id=" + d.id + "'>Change</a></td>" +
            "<td> <button class='btn btn-danger' onclick='deleteDisease(" + d.id + ")'>Delete</button></td>" +
            "</tr>";
    }
    ut += "</tabel>"
    $("#diseases").html(ut);
}


// Sends get-message for deletion of disease using id
function deleteDisease(id) {

    let url = "oblig/DeleteDisease?id=" + id;

    $.get(url, function (b) {
        if (b) {
            location.reload();
        }
        else {
            $("#err").html("Serverside error during delete")
        }

    });
}

