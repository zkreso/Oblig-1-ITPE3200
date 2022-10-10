var selecteddisease = window.location.search.substring(4);

$(function () {
    getDiseaseData();
    generateSymptomsTable();
});

function getDiseaseData() {
    const url = "oblig/GetDisease?id=" + selecteddisease;

    $.get(url, function (disease) {
        $("#id").val(selecteddisease); // må ha med id inn skjemaet, hidden i html
        $("#name").val(disease.name);
        $("#description").val(disease.description);
    });
}

function generateSymptomsTable() {
    const url1 = "oblig/GetRelatedSymptoms?id=" + selecteddisease;
    $.get(url1, function (symptoms) {
        let ut = printSymptoms(symptoms, true);
        $("#selectedsymptomsdiv").html(ut);
    });

    const url2 = "oblig/GetUnrelatedSymptoms?id=" + selecteddisease;

    $.get(url2, function (symptoms) {
        let ut = printSymptoms(symptoms);
        $("#notselectedsymptomsdiv").html(ut);
    });
}

function printSymptoms(symptoms, istrue) {
    let ut = "<table class='table'>" +
        "<thead><tr>" +
        "<th scope='col'>";
    if (istrue) {
        ut += "Selected symptoms";
    } else {
        ut += "Not selected symptoms";
    }
    ut += "</th > <th scope='col'></th>" +
        "</tr></thead><tbody>";
        
    for (let symptom of symptoms) {
        ut += "<tr>" +
            "<td>" + symptom.name + "</td><td>";
        if (istrue) {
            ut += "<button onclick='removeSymptom(" + symptom.id + ")' class='btn btn-danger'>Remove</button>";
        } else {
            ut += "<button onclick='addSymptom(" + symptom.id + ")' class='btn btn-secondary'>Add</button>";
        }
        ut += "</td></tr>";
    }
    ut += "</tbody></table>"
    return ut;
}

function addSymptom(inId) {
    const ds = {
        diseaseId : selecteddisease,
        symptomId : inId
    }

    $.post("oblig/CreateDiseaseSymptom", ds, function (OK) {
        if (OK) {
            generateSymptomsTable();
        }
        else {
            console.log("feil");
        }
    });
}

function removeSymptom(inId) {
    const ds = {
        diseaseId: selecteddisease,
        symptomId: inId
    }

    $.post("oblig/DeleteDiseaseSymptom", ds, function (OK) {
        if (OK) {
            generateSymptomsTable();
        }
        else {
            console.log("feil");
        }
    });
}

function saveChanges() {
    const disease = {
        id: $("#id").val(),
        name: $("#name").val(),
        description: $("#description").val(),
    };

    $.post("oblig/UpdateDisease", disease, function (OK) {
        if (OK) {
            window.location.href = 'diseaselist.html';
        }
        else {
            $("#feil").html("Feil i db - prøv igjen senere");
        }
    });
}
