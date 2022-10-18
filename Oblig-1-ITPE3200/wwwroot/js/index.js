$(() => {
    symptom();
})


const symptom = () => {
    $.get("kalkulator/getSymptomer", symptomList => {
        format(symptomList);
    })
}

const format = symptomList => {
    let ut = `<select class="form-control form-control-lg" id="symptom" onchange="getSymptom()" ><option disabled selected value>Velg symptomer</option>`;
    for (let s of symptomList) {
        ut += `<option value=${s.symptomId}${s.symptomNavn}>${s.symptomNavn}</option>`;
    }
    ut += "</select>";

    $("#kalkulator").html(ut);
}

let sList = "";
let symptomer = [];
const getSymptom = () => {
    let symptom = $("#symptom").val().substring(1);
    sList += symptom + "\n";
    $("#symptomList").html(sList);
    /** const symptomItem = {
        symptomId: $("#symptom").val().substring(0, 1),
        symptomNavn: $("#symptom").val().substring(1),
    } 
    symptomer.push($("#symptom").val());**/
    console.log(symptomer);
}

let dList = "";
const diagnose = () => {
    const symptom = {
        symptomId: $("#symptom").val().substring(0, 1),
        symptomNavn: $("#symptom").val().substring(1),
    }
    $.get("kalkulator/getDiagnoser", symptom, diagnoseList => {
        for (let d of diagnoseList) {
            dList += d.diagnoseNavn + " - " + d.description + "\n";
            $("#diagnoseList").html(dList);
        }
    })
    dList = "";
    sList = "";
    $("#symptom")[0].selectedIndex = 0;
}