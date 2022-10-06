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

    // Gathering symptoms that are related to disease
    $.get(url, function (tSymptoms) {
        $.get("oblig/GetAllSymptoms", function (symptoms) {

            for (let s in symptoms.name) {
                if (tSymptoms.name.includes(s)) {
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

    $("#symptoms").html(ut);
}