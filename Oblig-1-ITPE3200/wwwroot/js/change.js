// Places values from current state disease into fields
$(function () {
    // Gets diesease id
    const id = window.location.search.substring(1);
    let url = "oblig/GetDisease?" + id;

    $.get(url, function (d) {
        $("#id").val(d.id); // må ha med id inn skjemaet, hidden i html
        $("#name").val(d.name);
        $("#description").val(d.description);
    });

    getAllSymptoms(id);
});


function changeDisease() {
    const urlId = window.location.search.substring(1);
    const id = urlId.replace(/\D/g, "");
    const s = [];

    $("input[type='checkbox']").each(function () {
        if ($(this).is(":checked")) {
            let sid = $(this).attr('id');
            sid = sid ;

            $("#symptoms label").each(function () {
                if ($(this).attr('id') == sid) {

                    s[sid - 1] = {
                        id: sid,
                        name: $(this).attr('value'),
                    };

                }
            });

        }
    });

    const d = {
        id: $("#id").val(),
        name: $("#name").val(),
        description: $("#description").val(),
    };

    $.post("oblig/ChangeDisease", {d, s}, function (OK) {
        if (OK) {
            window.location.href = 'admin.html';
        }
        else {
            $("#err").html("Something wrong happened in change in db");
        }
    })
}


// Gathering all symptoms from db
function getAllSymptoms(id) {
    let url = "oblig/GetSymptomsDisease?" + id;

    // Getting symptoms that are related to disease
    $.get(url, function (tSymptoms) {
        // Getting all symptoms from symptoms-table
        $.get("oblig/GetAllSymptoms", function (symptoms) {

            // Running through all symtoms from symptoms-table
            for (let i = 0; i < symptoms.length; i++) {

                // Setting s as the name of the symptom 
                let s = symptoms[i];
                let b = false;

                // Running through the true symptoms list to check if s is in there
                for (let j = 0; j < tSymptoms.length; j++){

                    // Saving the true symtom as tS
                    let tS = tSymptoms[j];

                    // Checking if a match with s, if so set b to true and break out of for-loop
                    if (tS.id == s.id) {
                        b = true;
                        break;
                    }
                }

                // if tS was found in symptoms list, formatSymptoms with "checked" checkbox
                if (b) {
                    formatSymptom(s, true);
                }
                else {
                    formatSymptom(s, false);
                }
            }
        });
    }); 


}


// Formatting symptoms for html
function formatSymptom(s, b) {
    let ut = "";

    if (b) {
        ut += '<input type="text" value="' + s.id + '" style="display: none;"/>'
        ut += '<label id="' + s.id + '" value="' + s.name + '">' + s.name + '</label>'
        ut += '<input id="' + s.id + '" type="checkbox" checked />'
        ut += '<br>'
    }
    else {
        ut += '<input type="text" value="' + s.id + '" style="display: none;"/>'
        ut += '<label id="' + s.id + '" value="' + s.name +'">' + s.name + '</label>'
        ut += '<input id="'+ s.id +'" type="checkbox" />'
        ut += '<br>'
    }

    $("#symptoms").append(ut);
}