$(() => {
    hentSykdomListe();
});

const hentSykdomListe = () => {
    $.get("Kalkulator/HentSykdom", (sykdomListe) => formater(sykdomListe));
}

const formater = (sykdomListe) => {
    let liste = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Name</th>" +
        "<th>Description</th>" +
        "<th>Symptoms</th>" +
        "<th></th>" +
        "<th></th>" +
        "</tr>";

    for (let s of sykdomListe) {
        liste += "<tr>" +
            "<td>" + s.navn + "</td>" +
            "<td>" + s.beskrivelse + "</td>" +
            "<td>" + s.symptomListe + "</td>" +
            "<td><a href='endre.html?id=" + s.id + "' class='btn btn-primary'>Edit</a></td>" +
            "<td><button onclick='slett(" + s.id + ")' class='btn btn-danger'>Delete</button></td>" +
            "</tr>";
    }
    liste += "</table>";

    $("#visListe").html(liste);
}

const slett = (id) => {
    const url = "Kalkulator/Slett?id=" + id;

    $.get(url, (ok) => {
        if (ok) {
            window.location.href = 'admin.html';
        }
        else {
            $("#feil").html("Error in the database.");
        }
    });
};
