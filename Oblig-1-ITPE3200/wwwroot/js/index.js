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


// funke ikk
function findMatchingDisease() {
    let ids = [];

    $("#selected button").each(function () {
        ids.push(this.id);
    });

    $.post("oblig/FindMatchingDisease", ids, function (d) {
        $("#result").html(d.name);
    })

    /*$.ajax("oblig/FindMatchingDisease", {
        type: 'POST',
        traditional: true,
        data: {
            ids
        }
    })*/
}