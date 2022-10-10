let lagre = () => {
    const sykdom = {
        navn: $("#navn").val(),
        beskrivelse: $("#beskr").val()
        // $("#symptom").val(sykdom.symptomListe)
    }

    const url = "Kalkulator/Lagre";

    $.post(url, sykdom, (ok) => {
        if (ok) {
            window.location.href = 'admin.html';
        }
        else {
            $('#feil').html("Error in the database.");
        }
    });
};