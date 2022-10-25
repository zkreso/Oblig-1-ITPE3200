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
    let url = "kalkulator/getRelevantSymptoms?diagnoseid=" + id;
    $.get(url, symptoms => {
        formatSyptoms(symptoms);        
    })
}

const formatSyptoms = symptoms => {
    let table = `<table class="table table-striped"><tr><th>Relevant symptoms</th><th></th></tr>`;
    for (let s of symptoms) {
        table += `<tr><td>${s.symptomNavn}</td>
                    <td><button id=${s.symptomId} value="remove" class="btn btn-danger" onclick="remove(${s.symptomId})">Remove</button></td></tr>`;
    }
    table += "</table>";
    $("#table").html(table);
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

/**if (OK) {
            $("#tilbakeMelding").html("symptoms have updated.");
        }
        else {
            $("#tilbakeMelding").html("something wrong with the database, please try later");
        }
**/

