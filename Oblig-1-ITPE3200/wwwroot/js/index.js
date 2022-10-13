$(function () {
    getAllSymptoms();
});

function getAllSymptoms() {
    $.get("oblig/GetAllSymptoms", function (sList) {
        let ut = "";

        for (let i = 0; i < sList.length; i++) {

            ut += '<option id="' + sList[i].id + '">' + sList[i].name +'</option>';
        }
        $("#dList").append(ut);
    });
}

function addSymptom(s) {
    let id = $(s).attr("id");
    let name = $(s).val();

    $("#outSymptom").append(name+"<br>");
}