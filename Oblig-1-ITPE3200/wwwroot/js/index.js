$(() => {
    list();
})

const list = () => {
    $.get("kalkulator/index", list => {
        format(list);
    })
}

const format = list => {
    let ut = `<table class="table table-striped"><tr><th>Symptom</th><th>Diagnose</th><th>Description</th>
            <th></th><th></th><th></th></tr>`;
    for (let kunde of list) {
        ut += `<tr><td>${kunde.symptomNavn}</td><td>${kunde.symptomDiagnoser.diagnose.diagnoseNavn}</td><td>${kunde.symptomDiagnoser.diagnose.description}</td>
            
            <td><a class="btn btn-primary" href="endre.html?id=${kunde.id}">Endre</a></td>
            <td><a class="btn btn-danger" onclick="slett(${kunde.id})">Slett</a></td></tr>`;
    }
    ut += "</table>";

    $("#list").html(ut);
}