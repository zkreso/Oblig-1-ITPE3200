$(() => {
    // Get (old) disease info by its id from url and display it
    const id = window.location.search.substring(1);
    const url = "Kalkulator/HentEn?" + id;

    $.get(url, (sykdom) => {
        $("#id").val(sykdom.id);
        $("#navn").val(sykdom.navn);
        $("#beskr").val(sykdom.beskrivelse);
        // $("#symptom").val(sykdom.symptomListe)
    });
});

const endre = () => {
    const sykdom = {
        id: $("#id").val(), 
        navn: $("#navn").val(),
        beskrivelse: $("#beskr").val()
        // symptomListe: $("#symptom").val()
    }

    $.post("Kalkulator/Endre", sykdom, (ok) => {
        if (ok) {
            window.location.href = 'admin.html';
        }
        else {
            $("#feil").html("Error in the database.");
        }
    });
}