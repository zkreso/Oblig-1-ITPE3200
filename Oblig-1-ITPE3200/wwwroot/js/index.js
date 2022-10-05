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
    let ut = "<table class='table table-striped'>" +
        "<tr><th>Name</th><th>Symptoms</th><th>Edit</th><th>Delete</th></tr>";
    for (let d of diseases) {
        ut += "<tr>" +
            "<td>" + d.name + "</td>" +
            "<td>"
        for (let symptom of d.symptoms) {
            ut += symptom.name + ", ";
        }
        ut += "</td>" +
            "<td><a href='endre.html?id=" + d.id + "'>Edit</a></td>" +
            "<td><button class='btn btn-danger' onclick='deletedisease(" + d.id + ")'>Delete</button></td>" +
            "</td></tr>";
    }
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
