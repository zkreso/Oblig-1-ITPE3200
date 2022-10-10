$(function () {
    $.get("oblig/GetAllSymptoms", function (symptoms) {
        skrivUtSymptoms(symptoms);
    });
});

function skrivUtSymptoms(symptoms) {
    let ut = "";
    for (let symptom of symptoms) {
        ut += "<div class='form-check'>";
        ut += "<input class='form-check-input' name='symptoms' type='checkbox' value='" + symptom.id + "' id='" + symptom.name + "'>";
        ut += "<label class ='form-check-label' for='" + symptom.name + "'>" + symptom.name + "</label>";
        ut += "</div>";
    }
    $("#symptomsdiv").html(ut);
}

function createDisease() {
    const disease = {
        name: $("#name").val(),
        description: $("#description").val(),
        diseaseSymptoms : createDsList()
    };

    $.post("oblig/CreateDisease", disease, function (OK) {
        if (OK) {
            window.location.href = 'diseaselist.html';
        }
        else {
            $("#feil").html("Feil i db - prøv igjen senere");
        }
    });
}

function createDsList() {
    let dsArray = [];
    let formData = document.getElementsByName("symptoms");
    for (let entry of formData) {
        if (entry.checked) {
            const ds = {
                symptomId : entry.value,
            };
            dsArray.push(ds);
        }
    }
    return dsArray;
}
