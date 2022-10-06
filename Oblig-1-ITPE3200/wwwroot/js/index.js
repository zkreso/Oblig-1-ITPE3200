$(function () {
    getalldiseases();
    // getallsymptoms();
})

function getalldiseases() {
    $.get("oblig/GetAllDiseases", function (diseases) {
        skrivUtDiseases(diseases);
    });
}

function skrivUtDiseases(diseases) {
    let ut = "<table class='table table-sm'>" +
        "<thead><tr>" +
        "<th scope='col'>Name</th><th scope='col'>Symptoms</th><th scope='col'>Edit</th><th scope='col'>Delete</th>" +
        "</tr></thead><tbody>"
    for (let d of diseases) {
        ut += "<tr>" +
            "<th scope='row'>" + d.name + "</th>" +
            "<td>"
        for (let symptom of d.symptoms) {
            ut += symptom.name + ", ";
        }
        ut += "</td>" +
            "<td><a href='endre.html?id=" + d.id + "' class='btn btn-primary'>Edit</a></td>" +
            "<td><button class='btn btn-danger' onclick='deletedisease(" + d.id + ")'>Delete</button></td>" +
            "</td></tr>";
    }
    ut += "</tbody></table>"
    $("#diseases").html(ut);
}

function deletedisease(id) {
    const url = "oblig/DeleteDisease?id=" + id;
    $.get(url, function (OK) {
        if (OK) {
            window.location.href = 'index.html';
        }
        else {
            $("#feil").html("Feil i db - prøv igjen senere");
        }
    });
};
