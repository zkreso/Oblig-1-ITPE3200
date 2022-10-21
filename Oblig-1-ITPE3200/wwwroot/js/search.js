// Global variables

// Keeps track of selected symptoms (as objects). Used for:
// - calculating diagnosis
// - printing selected symptoms
// - excluding from list of all symptoms
var selectedSymptoms = []; 

// Keeps track of the value of the search box
// Used for filtering the list of all symptoms
// Empty string is treated as null by server
searchString = "";

// Initialize
$(function () {
    generateSymptomsList();
})

// Functions

// "Selects" ie. adds a symptom to the selected symptoms
function select(sid, sname) {
    let newSymptom = {
        "id": sid,
        "name": sname
    }
    selectedSymptoms.push(newSymptom);

    printSelectedSymptoms();
    calculateDiagnosis();
    generateSymptomsList();
}

// "Deselects" ie. removes a symptom from selected symptoms
function deselect(sid) {
    let i = selectedSymptoms.findIndex(s => s.id == sid);
    selectedSymptoms.splice(i, 1);

    printSelectedSymptoms();
    calculateDiagnosis();
    generateSymptomsList();
}

// Clears search box
function clearSearch() {
    $("#searchBox").val("");
    generateSymptomsList();
}

// Gets the list of all symptoms from server
// If some symptoms are selected, they are excluded
// Also excludes symptoms not matching the search string
function generateSymptomsList() {

    // Makes sure to get the current value of the search box
    searchString = $("#searchBox").val();

    // Generates array of symptom id's from the selected objects
    let symptomIds = [];

    if (selectedSymptoms.length > 0) {
        for (let symptom of selectedSymptoms) {
            symptomIds.push(symptom.id);
        }
    }

    // Calls getall method with filters and passes results to print function
    $.post("oblig/GetAllSymptoms", $.param({ symptomIds, searchString }, true), function (symptoms) {
        printAllSymptoms(symptoms);
    });
}

// Generates table from received objects
function printAllSymptoms(symptoms) {
    let ut = "<table class='table table-hover'><thead><tr>" +
        "<thead><tr>" +
        "<th scope='col'>Id</th><th scope='col'>Name</th><th scope='col'>Select</th>" +
        "</tr></thead><tbody>";
    for (let symptom of symptoms) {
        ut += "<tr>" +
            "<th scope='row'>" + symptom.id + "</td>" +
            "<td>" + symptom.name + "</td>" +
            "<td><a href='#' onclick='select(" + symptom.id + ", \"" + symptom.name + "\")'>Select</td></tr>";
    }
    $("#symptoms").html(ut);
}

// Generates display of selected symptoms. Shows "none" if none are selected
function printSelectedSymptoms() {
    let ut = "";

    if (selectedSymptoms.length == 0) {
        ut += "<div class='text-muted'>None</div>";
    }

    for (let symptom of selectedSymptoms) {
        ut += "<button type='button' class='btn btn-primary m-1' onclick='deselect(" + symptom.id + ")'>" +
            "<i class='bi bi-x-circle text-light'></i> " + symptom.name + "</button>";
    }

    $("#selectedsymptomsdiv").html(ut);
}

// Calculates diagnosis from selected symptoms
function calculateDiagnosis() {

    // If none are selected, returns without sending anything to server
    if (selectedSymptoms.length == 0) {
        let ut = "<div class='text-muted'>No symptoms selected - please select one or more symptoms from the symptom list</div>";
        $("#results").html(ut);
        return;
    }

    // Otherwise, generates array of symptom id's from the selected objects
    let symptomIds = [];

    for (let symptom of selectedSymptoms) {
        symptomIds.push(symptom.id);
    }

    // Gets diseases containing symptoms matching all the selected ones
    $.post("oblig/SearchDiseases", $.param({ symptomIds }, true), function (diseases) {

        // If no matches, shows mesage to user and returns
        if (diseases.length == 0) {
            let ut = "<div class='text-danger'>No matching diseases</div>";
            $("#results").html(ut);
            return;
        }

        // Otherwise, creates table of matching diseases
        let ut = "<table class='table'>" +
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
    });
}