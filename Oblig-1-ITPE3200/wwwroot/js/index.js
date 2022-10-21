$(function () {
    getAllSymptoms();
});

function getAllSymptoms() {
    $.get("oblig/GetAllSymptoms", function (sList) {
        let ut = "";

        for (let i = 0; i < sList.length; i++) {

            ut += '<li id="' + sList[i].id + '">' + sList[i].name +'</li>';
        }
        $("#symptoms").append(ut);
    });
}

function addSymptom(s) {
    let id = $(s).attr("id");
    let name = $(s).val();

    $("#outSymptom").append(name+"<br>");
}

function searchSymptom() {
    var input = document.getElementById("searchInput");
    var filter = input.value.toUpperCase();

    var ul = document.getElementById("symptoms");
    var li = ul.getElementsByTagName('li');
    var a;

    for (let i = 0; i < li.length; i++) {
        a = li[i].innerHTML;

        if (a.toUpperCase().indexOf(filter) > -1) {
            li[i].style.display = "";
        }
        else {
            li[i].style.display = "none";
        }
    }
}

function findMatchingDisease() {

    var str = $("#outSymptom").html().split("<br>");

    // Removing spaces and \n in the strings from the div
    for (let i = 0; i < str.length; i++) {
        let s = str[i];
        s = s.replace(/\s/g, '');
        str[i] = s;

        // If string is empty, remove from list
        if (s == "") {
            str.splice(i, 1);
        }
    }

    // SOMETHING WRONG IN HERE
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