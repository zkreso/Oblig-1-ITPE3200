$(function () {
    getalldiseases();
    getallsymptoms();
})

function getalldiseases() {
    $.get("oblig/GetAllDiseases", function (diseases) {
        skrivUtDiseases(diseases);
    });
}

function skrivUtDiseases(diseases) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Name</th><th></th><th></th>" +
        "</tr>";
    for (let d of diseases) {
        ut += "<tr>" +
            "<td>" + d.name + "</td>" +
            "<td> <a class='btn btn-primary' href='change.html?id=" + d.id + "'>Change</a></td>" +
            "<td> <button class='btn btn-danger' onclick='slettKunde(" + d.id + ")'>Delete</button></td>" +
            "</tr>";
    }
    ut += "</tabel>"
    $("#diseases").html(ut);
}

