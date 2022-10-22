// Global variables
var searchString = "";

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

    // Makes sure to get the current value of the search box
    searchString = $("#searchBox").val();

    let url = "oblig/GetAllDiseases?searchString=" + searchString;

    $.get(url, function (diseases) {
        skrivUtDiseases(diseases);
    });
}

function skrivUtDiseases(diseases) {
    let ut = "<table class='table'>" +
        "<thead><tr>" +
        "<th scope='col'>Id</th><th scope='col'>Name</th><th scope='col'>Symptoms</th><th scope='col'></th>" +
        "</tr></thead><tbody>"
    for (let d of diseases) {
        ut += "<tr>" +
            "<th scope='row'>" + d.id + "</th>" +
            "<td>" + d.name + "</td>" +
            "<td>";
        for (let symptom of d.symptoms) {
            ut += symptom + ", ";
        }
        ut += "</td>" +
            "<td><a href='disease.html?id=" + d.id + "' class='link-primary'>View</a> | " +
            "<a href='endre.html?id=" + d.id + "' class='link-primary'>Edit</a> | " +
            "<a href='#' onclick='deletedisease(" + d.id + ")' class='link-danger'>Delete</a</td ></tr > ";
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
