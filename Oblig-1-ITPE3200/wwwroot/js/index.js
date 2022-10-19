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

function findMatchingDisease() {

    /// CANT GATHER INFORMATION FROM LIST 
    var str = $("#outSymptom").html().split("<br>");
    str = str.split("\n");

    const symptoms = formToSymptom(str);

    $.post("oblig/FindMatchingDisease", symptoms, function (d) {
        $("#answer").html(d.name); 
    });

}

function formToSymptom(nameList) {
    const symptomsList = [];

    $.get("oblig/GetAllSymptoms", function (symptoms) {
        for (let i = 0; i < symptoms.length; i++) {
            let s = symptoms[i];

            let j = 0;
            for (n in nameList) {
                if (n == s.name) {
                    symptomsList[j] = s;
                }

                j++;
            }
        }
    });

    return symptomsList;
}