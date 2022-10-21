$(() => {
    list();
})

const list = () => {
    $.get("kalkulator/getDiagnoser", list => {
        format(list);
    })
}

const format = list => {
    let ut = `<table class="table table-striped"><tr><th>Diagnose</th><th>Description</th>
            <th></th><th></th></tr>`;
    for (let d of list) {
        ut += `<tr><td>${d.diagnoseNavn}</td><td>${d.description}</td>`;
            
        ut += `<td><a class="btn btn-primary" href="edit.html?id=${d.diagnoseId}">Edit</a></td>
            <td><a class="btn btn-danger" onclick="slett(${d.diagnoseId})">Delete</a></td></tr>`;
    }
    ut += "</table>";

    $("#dashboard").html(ut);
}

const slett = (diagnoseId) => {
    $.post("kalkulator/slettEnDiagnoser?diagnoseId=" + diagnoseId, () => {
        window.location.href = "admin.html";
    })
}