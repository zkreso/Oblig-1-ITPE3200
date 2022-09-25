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
    for (let d of diseases) {
        ut += d.name + "<br>"
        ut += "<ul>";
        for (let ds of d.diseaseSymptoms) {
            ut += "<li>" + ds.symptom.name + "</li>";
        }
        ut += "</ul>"
    }
    $("#diseases").html(ut);
}


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
        for (let ds of s.diseaseSymptoms) {
            ut += "<li>" + ds.disease.name + "</li>";
        }
        ut += "</ul>"
    }
    $("#symptoms").html(ut);
}
