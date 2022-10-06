$(function () {
    // Gets diesease id

    const id = window.location.search.substring(1);
    let url = "oblig/GetDisease?" + id;

    $.get(url, function (d) {
        $("#id").val(d.id); // må ha med id inn skjemaet, hidden i html
        $("#name").val(d.name);
    });

    getSymptomsDisease(id);
});


function changeDisease() {

}

function getSymptomsDisease(id) {

    let url = "oblig/GetSymptomsDisease?" + id;

    $.get(url, function (symptoms) {
        skrivUtSymptoms(symptoms);
    });
}

function getAllSymptoms() {
    $.get("oblig/GetAllSymptoms", function (symptoms) {
        skrivUtSymptoms(symptoms);
    });
}

function skrivUtSymptoms(symptoms) {
    let ut = "";
    for (let s of symptoms) {
        ut += s.name + "<br>"
    }
    $("#symptoms").html(ut);
}