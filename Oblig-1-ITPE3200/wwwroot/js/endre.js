// Global variables

const selecteddisease = window.location.search.substring(4);
var selectedSymptoms = []; // Objects - used by client only
var symptomIds = []; // Just the id's - used by server only
var orderBy = "idAscending";
var pageNum = 1;

// Initialize

$(function () {
    getDiseaseData();
    generateSymptomsList();
});

// Functions

// Gets data of disease that's being edited
function getDiseaseData() {
    const url = "oblig/GetDisease?id=" + selecteddisease;

    $.get(url, function (disease) {
        $("#id").val(selecteddisease); // må ha med id inn skjemaet, hidden i html
        $("#name").val(disease.name);
        $("#description").val(disease.description);
    });

    const url2 = "oblig/GetRelatedSymptoms?id=" + selecteddisease;

    $.get(url2, function (symptoms) {
        for (let symptom of symptoms) {
            let newSymptom = {
                "id": symptom.id,
                "name": symptom.name
            }
            selectedSymptoms.push(newSymptom);
        }
        update();
    }).fail(() => {
        $("#feil").html("Feil i db, prøv senere.");
    })
}

// Update function called when a symptom is selected or deselected
function update() {
    // Remakes array of selected symptom id's
    symptomIds = [];

    if (selectedSymptoms.length > 0) {
        for (let symptom of selectedSymptoms) {
            symptomIds.push(symptom.id);
        }
    }

    printSelectedSymptoms(); // Refreshes display of selected symptoms
    // calculateDiagnosis();
    generateSymptomsList(); // Refreshes display of remaining (unselected) symptoms
}

function saveChanges() {
    const disease = {
        id: $("#id").val(),
        name: $("#name").val(),
        description: $("#description").val(),
        diseaseSymptoms : createDsList()
    };

    $.post("oblig/UpdateDisease", disease, function (OK) {
        if (OK) {
            window.location.href = 'diseaselist.html';
        }
        else {
            $("#feil").html("Feil i db - prøv igjen senere");
        }
    }).fail(() => {
        $("#feil").html("Feil i db, prøv senere.");
    })
}

function createDsList() {
    let dsArray = [];
    for (let s of selectedSymptoms) {
        const ds = {
            symptomId: s.id,
            diseaseId : selecteddisease
        };
        dsArray.push(ds);
    }
    return dsArray;
}
