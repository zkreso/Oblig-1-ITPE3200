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
    for (let sd of list) {
        ut += `<tr><td>${sd.symptom.symptomNavn}</td><td>${sd.diagnose.diagnoseNavn}</td><td>${sd.diagnose.description}</td>`;
        /**for (let s of symptom.symptomDiagnoser) {
            ut += `<td>${s.diagnose.diagnoseNavn}</td><td>${s.diagnose.description}</td>`;
        }**/
            
        ut += `<td><a class="btn btn-primary" href="endre.html?id=${sd.id}">Endre</a></td>
            <td><a class="btn btn-danger" onclick="slett(${sd.id})">Slett</a></td></tr>`;
    }
    ut += "</table>";

    $("#list").html(ut);
}