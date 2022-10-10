$(function () {
    getallsymptoms();
})

function getallsymptoms() {
    $.get("oblig/GetAllSymptoms", function (symptoms) {
        skrivUtSymptoms(symptoms);
    });
}

function skrivUtSymptoms(symptoms) {
    let ut = "";
    for (let symptom of symptoms) {
        ut += "<div class='form-check'>";
        ut += "<input class='form-check-input' name='symptoms' type='checkbox' value='" + symptom.id + "' id='" + symptom.name + "'>";
        ut += "<label class ='form-check-label' for='" + symptom.name + "'>" + symptom.name + "</label>";
        ut += "</div>";
    }
    $("#symptoms").html(ut);
}

function search() {
    
    // Creating array of symptom id's to send to server
    let formData = document.getElementsByName("symptoms");
    let symptomsArray = [];
    for (let entry of formData) {
        if (entry.checked) {
            symptomsArray.push(entry.value);
        }
    }

    // Creating table of matches to show to user
    let resultsUt = "<h3>Matching diseases:</h3>" +
        "<table class='table table-striped'>" +
        "<tr><th>Disease</th><th>Symptoms</th></tr>";

    $.post("oblig/SearchDiseases", $.param({ symptomsArray }, true), function (diseases) {
        // Check for empty results
        if (diseases == null) {
            $("#results").html(resultsUt);
            return;
        }

        for (let result of diseases) {
            resultsUt += "<tr><td>" +
                "<a href='disease.html?id=" + result.id + "' class='link-primary'>" + result.name + "</a>" +
                "</td><td>";
            for (let s of result.symptoms) {
                resultsUt += s.name + ", ";
            }
            resultsUt += "</td></tr>";
        }
        resultsUt += "</table>";
        $("#results").html(resultsUt);
    });
}