$(function () {
    getallsymptoms();
})

function getallsymptoms() {
    $.get("oblig/GetAllSymptoms", function (symptoms) {
        skrivUtSymptoms(symptoms);
    });
}

function skrivUtSymptoms(symptoms) {
    let ut = "<table class='table table-striped'>" +
        "<tr><th>Choose symptoms:</th><th></th></tr>";
    for (let s of symptoms) {
        ut += "<tr><td>" + s.name + "</td>";
        ut += "<td><input type='checkbox' name='symptoms' value='" + s.id + "'/></td></tr > ";
    }
    ut += "</table>";
    $("#symptoms").html(ut);
}

function search() {
    let resultsUt = "<h2>Matching diseases:</h2>" +
        "<table class='table table-striped'>" +
        "<tr><th>Disease</th><th>Symptoms</th></tr>";
    let formData = document.getElementsByName("symptoms");
    let symptomsArray = [];
    for (let entry of formData) {
        if (entry.checked) {
            symptomsArray.push(entry.value);
        }
    }
    // Source: Sending int array
    // https://stackoverflow.com/questions/5489461/pass-array-to-mvc-action-via-ajax
    $.post("oblig/SearchDiseases", $.param({ symptomsArray }, true), function (diseases) {
        if (diseases != null) {
            for (let result of diseases) {
                resultsUt += "<tr><td>" + result.name + "</td><td>";
                for (let s of result.symptoms) {
                    resultsUt += s.name + ", ";
                }
                resultsUt += "</td></tr>";
            }
            resultsUt += "</table>";
        }
        $("#results").html(resultsUt);
    });
}