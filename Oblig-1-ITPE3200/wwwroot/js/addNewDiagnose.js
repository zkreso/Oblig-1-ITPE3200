$(() => {
})

const createEnDiagnose = () => {
    let diagnoseNavn = $("#diagnoseName").val();
    let description = $("#description").val();
    if (diagnoseNavn !== "" && description !== "") {
        const diagnose = {
            diagnoseNavn: diagnoseNavn,
            description: description
        }
        $.post("kalkulator/createEnDiagnose", diagnose, OK => {
            if (OK) {
                window.location.href = "admin.html";
            }
            else {
                $("#feil").html("something wrong with the database, please try later");
            }
        })

    }   
}


