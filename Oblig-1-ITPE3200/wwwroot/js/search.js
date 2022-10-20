var selectedSymptoms = [];

$(function () {
    getallsymptoms();
})

function getallsymptoms() {
    $.get("oblig/GetAllSymptoms", function (symptoms) {
        printAllSymptoms(symptoms);
    });
}

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

function select(sid, sname) {
    let newSymptom = {
        "name": sname,
        "id" : sid
    }
    selectedSymptoms.push(newSymptom);

    printSelectedSymptoms();
    search();
}

function deselect(sid) {
    let i = selectedSymptoms.findIndex(s => s.id == sid);
    selectedSymptoms.splice(i, 1);

    printSelectedSymptoms();
    search();
}

function printSelectedSymptoms() {
    let ut = "";
    if (selectedSymptoms.length == 0) {
        ut += "<p class='text-muted'>No symptoms selected</p>";
    }

    for (let symptom of selectedSymptoms) {
        ut += "<button type='button' class='btn btn-primary m-1' onclick='deselect(" + symptom.id + ")'>" +
            "<i class='bi bi-x-circle text-light'></i> " + symptom.name + "</button>";
    }
    $("#selectedsymptomsdiv").html(ut);
}

function search() {
    if (selectedSymptoms.length == 0) {
        let ut = "<div class='text-muted'>No symptoms selected</div>";
        $("#results").html(ut);
        return;
    }

    // Creating table of matches to show to user
    let resultsUt = "<table class='table'>" +
        "<tr><th>Name</th><th>Symptoms</th></tr>";

    let symptomsArray = [];
    for (let symptom of selectedSymptoms) {
        symptomsArray.push(symptom.id);
    }

    $.post("oblig/SearchDiseases", $.param({ symptomsArray }, true), function (diseases) {
        // Check for empty results
        if (diseases.length == 0) {
            let ut = "<div class='text-danger'>No matching diseases</div>";
            $("#results").html(ut);
            return;
        }

        for (let result of diseases) {
            resultsUt += "<tr><td>" +
                "<a href='disease.html?id=" + result.id + "' class='link-primary'>" + result.name + "</a>" +
                "</td><td>";
            for (let s of result.symptoms) {
                resultsUt += s + ", ";
            }
            resultsUt += "</td></tr>";
        }
        resultsUt += "</table>";
        $("#results").html(resultsUt);
    });
}