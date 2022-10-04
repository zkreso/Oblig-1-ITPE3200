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
        "<tr><th>Name</th><th>Symptoms</th><th>Change</th><th>Delete</th></tr>";
    for (let d of diseases) {
        ut += "<tr>" +
            "<td>" + d.name + "</td>" +
            "<td>"
        for (let symptom of d.symptoms) {
            ut += symptom.name + ", ";
        }
        ut += "</td>" +
            "<td><a class='btn btn-primary' href='endre.html?id=" + d.id + "' disabled>Update</a></td>" +
            "<td><button class='btn btn-danger' onclick='deletedisease(" + d.id + ")' disabled>Delete</button></td>" +
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


function getallsymptoms() {
    $.get("oblig/GetAllSymptoms", function (symptoms) {
        skrivUtSymptoms(symptoms);
    });
}

function skrivUtSymptoms(symptoms) {
    let ut = "";
    for (let s of symptoms) {
        ut += s.name + "<br>"
        ut += "<ul>";
        for (let disease of s.diseases) {
            ut += "<li>" + disease.name + "</li>";
        }
        ut += "</ul>"
    }
    $("#symptoms").html(ut);
}
