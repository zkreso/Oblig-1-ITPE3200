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
    let ut = "";
    for (let disease of diseases) {
        ut += disease.name;
        ut += "<ul>";
        for (let symptom of disease.symptoms) {
            ut += "<li>" + symptom.name + "</li>";
        }
        ut += "</ul>";
    }
    $("#diseases").html(ut);
}

function getallsymptoms() {
    $.get("oblig/GetAllSymptoms", function (symptoms) {
        skrivUtSymptoms(symptoms);
    });
}

function skrivUtSymptoms(symptoms) {
    let ut = "<ul>";
    for (let symptom of symptoms) {
        ut += "<li>" + symptom.name + "</li>";
    }
    ut += "</ul>";
    $("#symptoms").html(ut);
}