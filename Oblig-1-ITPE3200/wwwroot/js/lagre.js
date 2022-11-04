// Global variables

var selectedSymptoms = []; // Objects - used by client only
var symptomIds = []; // Just the id's - used by server only
var orderBy = "idAscending";
var pageNum = 1;

// Initialize

$(function () {
    generateSymptomsList();
});

function nextPage() {
    pageNum++;
    generateSymptomsList();
}

function prevPage() {
    pageNum--;
    generateSymptomsList();
}

// "Selects" ie. adds a symptom to the selected symptoms
function select(sid, sname) {
    let newSymptom = {
        "id": sid,
        "name": sname
    }
    selectedSymptoms.push(newSymptom);

    update();
}

// "Deselects" ie. removes a symptom from selected symptoms
function deselect(sid) {
    let i = selectedSymptoms.findIndex(s => s.id == sid);
    selectedSymptoms.splice(i, 1);

    update();
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

// Clears search box
function clearSearch() {
    $("#searchBox").val("");
    pageNum = 1; // reset page when search is cleared
    generateSymptomsList();
}

// Sorting functions
function sortByName() {
    if (orderBy === "nameAscending") {
        orderBy = "nameDescending";
    } else {
        orderBy = "nameAscending";
    }
    pageNum = 1; // reset page when sorting is changed
    generateSymptomsList();
}

function sortById() {
    if (orderBy === "idAscending") {
        orderBy = "idDescending";
    } else {
        orderBy = "idAscending";
    }
    pageNum = 1; // reset page when sorting is changed
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
    }).fail(() => {
        $("#feil").html("Feil i db, prøv senere.");
    })
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
