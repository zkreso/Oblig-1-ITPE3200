$(() => {
    getDiagnose();
    getSymptoms();
})

const id = window.location.search.substring(4);

const getDiagnose = () => {
    let url = "kalkulator/getEnDiagnose?diagnoseid=" + id; //has to have "diagnose" in url!!!!!!
    
    $.get(url, diagnose => {
        $("#id").val(diagnose.diagnoseId);
        $("#description").val(diagnose.description);
        $("#diagnoseNavn").html("Diagnose: " + diagnose.diagnoseNavn);
    })
}

const getSymptoms = () => {
    let url1 = "kalkulator/getRelevantSymptoms?diagnoseid=" + id;
    $.get(url1, symptoms => {
        formatRelevantSyptoms(symptoms);
    })

    let url2 = "kalkulator/getIrrelevantSymptoms?diagnoseid=" + id;
    $.get(url2, symptoms => {
        formatIrelevantSyptoms(symptoms);
    })
}

const formatRelevantSyptoms = (symptoms) => {
    let table = `<table class="table table-striped"><tr><th>Relevant symptoms</th><th></th></tr>`;
    for (let s of symptoms) {
        table += `<tr><td>${s.symptomNavn}</td>
                    <td><button class="btn btn-danger" onclick="remove(${s.symptomId})">Remove</button></td></tr>`;
    }
    table += "</table>";
    $("#relevantSyptomsTable").html(table);
}

const formatIrelevantSyptoms = symptoms => {
    let table = `<table class="table table-striped"><tr><th>Other symptoms</th><th></th></tr>`;
    for (let s of symptoms) {
        table += `<tr><td>${s.symptomNavn}</td>
                    <td><button class="btn btn-secondary" onclick="add(${s.symptomId})">Add</button></td></tr>`;
    }
    table += "</table>";
    $("#irrelevantSyptomsTable").html(table);
}

const remove = symptomId => {
    const symptomDiagnose = {
        diagnoseId: id,
        symptomId: symptomId
    }

    $.post("kalkulator/removeSymptomDiagnose", symptomDiagnose, OK => {
        if (OK) {
            getSymptoms();
        }
        else {
            $("#symptomStatus").html("something wrong with the database, please try later");
        }
    })
}

const add = symptomId => {
    const symptomDiagnose = {
        diagnoseId: id,
        symptomId: symptomId
    }

    $.post("kalkulator/addSymptomDiagnose", symptomDiagnose, OK => {
        if (OK) {
            getSymptoms();
        }
        else {
            $("#symptomStatus").html("something wrong with the database, please try later");
        }
    })
}

const addNewSymptom = () => {
    let newSymptom = $("#addNewSymptom").val();
    if (newSymptom !== "") {
        $.post("kalkulator/addNewSymptom?symptomNavn=" + newSymptom + "&diagnoseid=" + id, OK => {
            if (OK) {
                getSymptoms();
            }
            else {
                $("#symptomStatus").html("something wrong with the database, please try later");
            }
        })
        $("#addNewSymptom").val("");
    }
}

const confirm = () => {
    let description = $("#description").val();
    $.post("kalkulator/updateDescription?diagnoseid="+id+"&description="+description, OK => {
        if (OK) {
            window.location.href = "admin.html";
        }
        else {
            $("#tilbakeMelding").html("something wrong with the database, please try later");
        }
    })
}


