// Initialize
$(function () {
    getalldiseases();
})

// Functions
function clearSearch() {
    $("#searchBox").val("");
    getalldiseases();
}

function getalldiseases() {

    let url = "oblig/GetAllDiseases?searchString=" + $("#searchBox").val();

    $.get(url, function (diseases) {
        skrivUtDiseases(diseases);
    });
}

function skrivUtDiseases(diseases) {
    let ut = "<table class='table'>" +
        "<thead><tr>" +
        "<th scope='col'>Name</th><th scope='col'>Symptoms</th><th scope='col'></th>" +
        "</tr></thead><tbody>"

    for (let d of diseases) {
        ut += "<tr>" +
            // "<th scope='row'>" + d.id + "</th>" +
            "<td>" + d.name + "</td>" +
            "<td>";

        for (let symptom of d.symptoms) {
            ut += symptom + ", ";
        }
        ut += "</td>" +
            "<td>" +
            "<a href='disease.html?id=" + d.id + "' class='btn btn-primary' role='button'>View</a> " +
            "<a href='endre.html?id=" + d.id + "' class='btn btn-primary' role='button'>Edit</a> " +
            "<a href='#' onclick='deletedisease(" + d.id + ")' class='btn btn-danger' role='button'>Delete</a</td></tr>";
    }
    ut += "</tbody></table>"
    $("#diseases").html(ut);
}

function deletedisease(id) {
    const url = "oblig/DeleteDisease?id=" + id;
    $.get(url, function (OK) {
        if (OK) {
            window.location.href = 'diseaselist.html';
        }
        else {
            $("#feil").html("Feil i db - prøv igjen senere");
        }
    });
};
