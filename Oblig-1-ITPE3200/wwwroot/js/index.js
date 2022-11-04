// Global variables

var selectedSymptoms = []; // Objects - used by client only
var symptomIds = []; // Just the id's - used by server only
var orderBy = "idAscending";
var pageNum = 1;

// Initialize

$(function () {
    generateSymptomsList();
})

// Functions

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
    calculateDiagnosis(); // Recalculates diagnosis
    generateSymptomsList(); // Refreshes display of remaining (unselected) symptoms
}

// Calculates diagnosis from selected symptoms
function calculateDiagnosis() {

    // If none are selected, returns without sending anything to server
    if (selectedSymptoms.length == 0) {
        let ut = "<div class='text-muted'>No symptoms selected - please select one or more symptoms from the symptom list</div>";
        $("#results").html(ut);
        return;
    }

    // Gets the diseases that contain every id in the array symptomIds
    $.post("oblig/SearchDiseases", $.param({ symptomIds }, true), function (diseases) {

        // If no matches, shows mesage instead of table
        if (diseases.length == 0) {
            let ut = "<div class='text-danger'>No matching diseases</div>";
            $("#results").html(ut);
            return;
        }

        // Otherwise, creates table of matching diseases
        let ut = "<h6 class='text-primary'>" + diseases.length + " result(s) found</h6>" +
            "<table class='table'>" +
            "<tr><th>Name</th><th>Symptoms</th></tr>";

        for (let result of diseases) {
            ut += "<tr><td>" +
                "<a href='disease.html?id=" + result.id + "' class='link-primary'>" + result.name + "</a>" +
                "</td><td>";
            for (let s of result.symptoms) {
                ut += s + ", ";
            }
            ut += "</td></tr>";
        }
        ut += "</table>";

        $("#results").html(ut);
    }).fail(() => {
        $("#feil").html("Feil i db, prøv senere.");
    })
}