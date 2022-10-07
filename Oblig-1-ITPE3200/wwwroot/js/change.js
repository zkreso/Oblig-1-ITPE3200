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
                let s = symptoms[i].name;
                let b = false;

                // Running through the true symptoms list to check if s is in there
                for (let j = 0; j < tSymptoms.length; j++){

                    // Saving the true symtom as tS
                    let tS = tSymptoms[j].name;

                    // Checking if a match with s, if so set b to true and break out of for-loop
                    if (tS == s) {
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
        ut += '<label>' + s + '</label>'
        ut += '<input type="checkbox" checked/>'
        ut += '<br>'
    }
    else {
        ut += '<label>' + s + '</label>'
        ut += '<input type="checkbox" />'
        ut += '<br>'
    }

    $("#symptoms").append(ut);
}