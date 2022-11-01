// Global variables

var selectedSymptoms = []; // Objects - used by client only
var symptomIds = []; // Just the id's - used by server only
var orderBy = "idAscending";
var pageNum = 1;

// Initialize

$(function () {
    generateSymptomsList();
});

// Functions

function generateSymptomsList() {

    // Making options object to send to server
    const options = {
        orderByOptions: orderBy,
        searchString: $("#searchBox").val(),
        pageSize: $("#pagesizeselect").val(),
        pageNum: pageNum
    };

    for (let i = 0; i < symptomIds.length; i++) {       // Workaround for not being able to
        var propertyname = "symptomIds[" + i + "]";     // send an array inside an object
        options[propertyname] = symptomIds[i];
    }
    // options object done

    $.post("oblig/GetSymptomPage", options, function (page) {
        // Sets the page number to whatever the server calculated, in case it went out of range
        pageNum = page.pageData.pageNum;

        // Show number of entries
        let ut = "Showing <strong>" + page.symptomList.length + "</strong> of <strong>" + page.pageData.numEntries + "</strong> symptoms.";

        // Table of symptoms
        ut += "<table class='table table-hover'><thead><tr>" +
            "<thead><tr>" +
            // "<th scope='col'><a href='#' class='link-primary' onclick='sortById()'>Id</a></th>" +
            "<th scope='col'><a href='#' class='link-primary' onclick='sortByName()'>Name</a></th>" +
            "<th scope='col'>Select</th>" +
            "</tr></thead><tbody>";
        for (let symptom of page.symptomList) {
            ut += "<tr>" +
                // "<th scope='row'>" + symptom.id + "</td>" +
                "<td>" + symptom.name + "</td>" +
                "<td><a href='#' onclick='select(" + symptom.id + ", \"" + symptom.name + "\")'>Select</td></tr>";
        }
        $("#symptoms").html(ut);

        // Page navigation
        let utnav = "";

        if (pageNum == 1) {
            utnav += "<li class='page-item disabled'><span class='page-link'>Previous page</span></li>";
        } else {
            utnav += "<li class='page-item'><a class='page-link' href=#' onclick='prevPage()'>Previous page</a></li>";
        }

        for (let i = 1; i < page.pageData.numPages + 1; i++) {
            if (i == pageNum) {
                utnav += "<li class='page-item active' aria-current='page'><span class='page-link'>" + i + "</span>";
            } else {
                utnav += "<li class='page-item'><a class='page-link' href='#' onclick='goToPage(" + i + ")'>" + i + "</a></li>";
            }
        }

        if (pageNum == page.pageData.numPages) {
            utnav += "<li class='page-item disabled'><span class='page-link'>Next page</span></li>";
        } else {
            utnav += "<li class='page-item'><a class='page-link' href=#' onclick='nextPage()'>Next page</a></li>";
        }

        $("#pagenav").html(utnav);
    });
}

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
    pageNum = 1; // Reset page when search is cleared
    generateSymptomsList();
}

// Sorting functions
function sortByName() {
    if (orderBy === "nameAscending") {
        orderBy = "nameDescending";
    } else {
        orderBy = "nameAscending";
    }
    pageNum = 1; // Reset page when sorting is changed
    generateSymptomsList();
}

function sortById() {
    if (orderBy === "idAscending") {
        orderBy = "idDescending";
    } else {
        orderBy = "idAscending";
    }
    pageNum = 1; // Reset page when sorting is changed
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
