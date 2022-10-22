// Global variables

var selectedSymptoms = []; // Objects - used by client only
var symptomIds = []; // Just the id's - used by server only
var searchString = "";
var orderBy = "idAscending";

// Initialize

$(function () {
    generateSymptomsList();
})

// Functions

function generateSymptomsList() {

    // Makes sure to get the current value of the search box
    searchString = $("#searchBox").val();

    $.post("oblig/GetAllSymptoms", $.param({ symptomIds, searchString, orderBy }, true), function (symptoms) {

        let ut = "<table class='table table-hover'><thead><tr>" +
            "<thead><tr>" +
            "<th scope='col'><a href='#' class='link-primary' onclick='sortById()'>Id</a></th>" +
            "<th scope='col'><a href='#' class='link-primary' onclick='sortByName()'>Name</a></th>" +
            "<th scope='col'>Select</th>" +
            "</tr></thead><tbody>";

        for (let symptom of symptoms) {
            ut += "<tr>" +
                "<th scope='row'>" + symptom.id + "</td>" +
                "<td>" + symptom.name + "</td>" +
                "<td><a href='#' onclick='select(" + symptom.id + ", \"" + symptom.name + "\")'>Select</td></tr>";
        }

        $("#symptoms").html(ut);
    });
}

// "Selects" ie. adds a symptom to the selected symptoms
function select(sid, sname) {
    let newSymptom = {
        "id": sid,
        "name": sname
    }
    selectedSymptoms.push(newSymptom);

    update(); // Updates what needs to be updated when selection changes
}

// "Deselects" ie. removes a symptom from selected symptoms
function deselect(sid) {
    let i = selectedSymptoms.findIndex(s => s.id == sid);
    selectedSymptoms.splice(i, 1);

    update(); // Updates what needs to be updated when selection changes
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
    calculateDiagnosis(); // Recalculates diagnosis
    generateSymptomsList(); // Refreshes display of remaining (unselected) symptoms
}

// Clears search box
function clearSearch() {
    $("#searchBox").val("");
    generateSymptomsList();
}

// Sorting functions
function sortByName() {
    if (orderBy === "nameAscending") {
        orderBy = "nameDescending";
    } else {
        orderBy = "nameAscending";
    }
    generateSymptomsList();
}

function sortById() {
    if (orderBy === "idAscending") {
        orderBy = "idDescending";
    } else {
        orderBy = "idAscending";
    }
    generateSymptomsList();
}

// Generates display of selected symptoms. Shows "none" if none are selected
function printSelectedSymptoms() {
    let ut = "";

    if (selectedSymptoms.length == 0) {
        ut += "<div class='text-muted'>None</div>";
    }

    for (let symptom of selectedSymptoms) {
        ut += "<button type='button' class='btn btn-lg btn-primary m-1' onclick='deselect(" + symptom.id + ")'>" +
            "<i class='bi bi-trash text-light'></i> " + symptom.name + "</button>";
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
    });
}