var selecteddisease = window.location.search.substring(4);

$(function () {
    getDiseaseData();
});

function getDiseaseData() {
    const url = "oblig/GetDisease?id=" + selecteddisease;

    $.get(url, function (disease) {
        $("#name").html(disease.name);
        $("#description").html(disease.description);
        let ut = "<ul>";
        for (let s of disease.symptoms) {
            ut += "<li>" + s + "</li>";
        }
        ut += "</ul>";
        $("#symptoms").html(ut);
    });
}