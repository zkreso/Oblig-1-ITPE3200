$(function () {
    getDiseaseData();

    $.get("oblig/GetAllSymptoms", function (symptoms) {
        skrivUtSymptoms(symptoms);
    });
});

function getDiseaseData() {
    const id = window.location.search.substring(1);
    const url = "oblig/GetDisease?" + id;

    $.get(url, function (disease) {
        $("#id").val(disease.id); // må ha med id inn skjemaet, hidden i html
        $("#name").val(disease.name);
    });
}

function skrivUtSymptoms(symptoms) {
    const id = $("#id").val();
    let ut = "";
    for (let symptom of symptoms) {
        ut += "<div class='form-check'>";
        ut += "<input clss='form-check-input' name='symptoms' type='checkbox' value='" + symptom.id + "' id='" + symptom.name + "' ";
        for (let disease of symptom.diseases) {
            if (disease.id == id) {
                ut += "checked";
            }
        }
        ut += ">";
        ut += "<label class ='form-check-label' for='symptom" + symptom.id + "'>" + symptom.name + "</label>";
        ut += "</div>";
    }
    $("#symptomsdiv").html(ut);
}

function saveChanges() {
    const disease = {
        id: $("#id").val(),
        name: $("#name").val(),
        symptoms : createSymptomsList()
    };

    $.post("oblig/UpdateDisease", disease, function (OK) {
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
