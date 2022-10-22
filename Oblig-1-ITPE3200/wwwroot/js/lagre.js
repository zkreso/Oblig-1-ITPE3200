// Global variables

var selectedSymptoms = []; // Objects - used by client only
var symptomIds = []; // Just the id's - used by server only
var searchString = "";

// Initialize

$(function () {
    generateSymptomsList();
});

// Functions

function generateSymptomsList() {

    searchString = $("#searchBox").val(); // Makes sure to get the current value of the search box

    $.post("oblig/GetAllSymptoms", $.param({ symptomIds, searchString }, true), function (symptoms) {

        let ut = "<table class='table table-hover'><thead><tr>" +
            "<thead><tr>" +
            "<th scope='col'>Id</th><th scope='col'>Name</th><th scope='col'>Add</th>" +
            "</tr></thead><tbody>";

        for (let symptom of symptoms) {
            ut += "<tr>" +
                "<th scope='row'>" + symptom.id + "</td>" +
                "<td>" + symptom.name + "</td>" +
                "<td><a href='#' onclick='select(" + symptom.id + ", \"" + symptom.name + "\")'>Add</td></tr>";
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
    generateSymptomsList(); // Refreshes display of remaining (unselected) symptoms
}

// Clears search box
function clearSearch() {
    $("#searchBox").val("");
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
    for (let s of selectedSymptoms) {
        const ds = {
            symptomId : s.id
        };
        dsArray.push(ds);
    }
    return dsArray;
}
