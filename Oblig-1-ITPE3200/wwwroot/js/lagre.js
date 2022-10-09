$(function () {
    $.get("oblig/GetAllSymptoms", function (symptoms) {
        skrivUtSymptoms(symptoms);
    });
});

function skrivUtSymptoms(symptoms) {
    let ut = "";
    for (let symptom of symptoms) {
        ut += "<div class='form-check'>";
        ut += "<input clss='form-check-input' name='symptoms' type='checkbox' value='" + symptom.id + "' id='" + symptom.name + "'>";
        ut += "<label class ='form-check-label' for='symptom" + symptom.id + "'>" + symptom.name + "</label>";
        ut += "</div>";
    }
    $("#symptomsdiv").html(ut);
}

function createDisease() {
    const disease = {
        name: $("#name").val(),
        description: $("#description").val(),
        symptoms : createSymptomsList()
    };

    $.post("oblig/CreateDisease", disease, function (OK) {
        if (OK) {
            window.location.href = 'index.html';
        }
        else {
            $("#feil").html("Feil i db - prøv igjen senere");
        }
    });
}

function createSymptomsList() {
    let symptoms = [];
    let formData = document.getElementsByName("symptoms");
    for (let entry of formData) {
        if (entry.checked) {
            const symptom = {
                id : entry.value,
                name : entry.id
            };
            symptoms.push(symptom);
        }
    }
    return symptoms;
}
