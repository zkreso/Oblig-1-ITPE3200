$(function () {
    getAllSymptoms();
});

function getAllSymptoms() {
    $.get("oblig/GetAllSymptoms", function (sList) {
        formatSymptoms(sList);
    });
}


// ----- Formatting of objects ------

function formatSymptoms(sList) {
    let ut = "<table class='table' id='table'><thead>" +
        "<tr scope='col'><th>ID</th>" +
        "<th scope = 'col' > Name</th> " +
        "<th></th>" +
        "</tr></thead>";

    ut += "<tbody id='tbody'>";

    for (let i = 0; i < sList.length; i++) {
        let s = sList[i];
        ut += "<tr id='" + s.id + "'><th scope='row'>" + s.id + "</th>" +
            "<td>" + s.name + "</td>" +
            "<td><a href='#' id='" + s.id + "' onclick='addSymptom(this)'>Add</a></td>" +
            "</tr>";
    }

    ut += "</tbody></table>";

    $("#symptoms").html(ut);
}


// ----- Adding, removing different objects html -----

function addSymptom(a) {
    let ut = "<button id='" + a.id + "' type='button' class='btn btn-primary' onclick='remSympTag(this)'>";

    let url = "oblig/GetSymptom?id=" + a.id;
    $.get(url, function (s) {
        let name = s.name;

        ut += name + "</button>";

        $("#selected").append(ut);
    });

    let tr = $(a).closest('tr');
    $(tr).css("display", "none");
}

function remSympTag(button) {
    let id = button.id;

    $("tr").each(function () {
        if (this.id == id) {
            $(this).css("display", "");
        }
    });

    button.id = "NaN";
    $(button).css("display", "none");
}



// ------- Search functions and other later projects -------
function searchSymptom() {
    var input = document.getElementById("searchInput");
    var filter = input.value.toUpperCase();

    var tbody = document.getElementById("tbody");
    var trL = tbody.getElementsByTagName('tr');
    var a;

    for (let i = 0; i < trL.length; i++) {
        a = trL[i].innerHTML;

        if (a.toUpperCase().indexOf(filter) > -1) {
            $("#selected button").each(function () {

                // Checking if symptom is checked, then no show
                // Does not work with multiple selected symptoms
                if (trL[i].id == this.id) {
                    trL[i].style.display = "none";
                }
                else {
                    trL[i].style.display = "";
                }
            })
        }
        else {
            trL[i].style.display = "none";
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