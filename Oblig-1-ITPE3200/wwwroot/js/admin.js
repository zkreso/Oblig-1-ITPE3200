$(() => {
    list();
})

const list = () => {
    $.get("kalkulator/getSymptomDiagnoser", list => {
        format(list);
    })
}

const format = list => {
    let ut = `<table class="table table-striped"><tr><th>Diagnose</th><th>Description</th>
            <th></th><th></th></tr>`;
    for (let sd of list) {
        ut += `<tr><td>${sd.diagnose.diagnoseNavn}</td><td>${sd.diagnose.description}</td>`;
        /**for (let s of symptom.symptomDiagnoser) {
            ut += `<td>${sd.symptom.symptomNavn}</td><td>${s.diagnose.diagnoseNavn}</td><td>${s.diagnose.description}</td>`;
        }**/
            
        ut += `<td><a class="btn btn-primary" href="edit.html?id=${sd.id}">Edit</a></td>
            <td><a class="btn btn-danger" onclick="slett(${sd.id})">Delete</a></td></tr>`;
    }
    ut += "</table>";

    $("#dashboard").html(ut);
}