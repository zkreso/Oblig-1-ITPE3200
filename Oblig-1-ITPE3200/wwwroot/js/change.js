$(function () {
    // Gets diesease id

    const id = window.location.search.substring(1);
    const url = "oblig/GetDisease" + id;

    $.get(url, function (d) {
        $("#id").val(d.id); // må ha med id inn skjemaet, hidden i html
        $("#name").val(d.name);
    });

    getallsymptoms();
});


function changeDisease() {

}

function getallSymptomsDisease() {
    $.get("oblig/GetAllSymptomsId", function (symptoms) {
        skrivUtSymptoms(symptoms);
    });
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