$(() => {
    getSymptoms();
})

const getSymptoms = () => {
    $.get("kalkulator/GetSymptomer", symptoms => {
        formatSymptoms(symptoms);
    })
}

const formatSymptoms = symptoms => {
    let table = `<table class="table table-striped"><tr><th>Symptoms</th><th></th></tr>`;
    for (let s of symptoms) {
        table += `<tr><td>${s.symptomNavn}</td>
                    <td><button class="btn btn-secondary" onclick="add(${s.symptomId})">Add</button></td></tr>`;
    }
    table += "</table>";
    $("#symptomTable").html(table);
}

const createEnDiagnose = () => {
    const diagnose = {
        diagnoseNavn: $("#diagnoseName").val(),
        description: $("#description").val()
    }

    $.post("kalkulator/createEnDiagnose", diagnose, diagnoseModel => {
        format(diagnoseModel);        
    })
}

const format = diagnoseModel => {
    let table = `<table class="table table-striped"><tr><th>Name</th><td>${diagnoseModel.diagnoseNavn}</td></tr>`;
    table += `<tr><th>Description</th><td>${diagnoseModel.description}</td></tr>`;
    $("#table").html(table);
}
