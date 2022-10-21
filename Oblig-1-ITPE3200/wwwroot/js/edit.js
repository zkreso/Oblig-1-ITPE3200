$(() => {
    editTable();
})

const editTable = () => {
    let id = window.location.search.substring(1); // id = id=1
    let url = "kalkulator/getEnDiagnose?diagnose" + id; //has to have diagnose in url!!!!!!
    let ut = "";
    $.get(url, diagnoser => {
        for (let d of diagnoser) {
            $("#diagnoseNavn").html(d.diagnoseNavn);
            for (let navn of d.symptomNavnene) {
                ut += `<input class="symptomCheckbox" type="checkbox" id=${navn} value=${navn} onchange="uncheck()" checked>
                    <label for=${d.diagnoseId}>${navn}</label><br>`
            }            
        }
        $("#table").html(ut);
    })
}

const uncheck = () => {
    var not_checked = []
    $("input[type='checkbox']:not(:checked)").each(function () {
        not_checked.push($(this).val());
    });
    console.log(not_checked);
}