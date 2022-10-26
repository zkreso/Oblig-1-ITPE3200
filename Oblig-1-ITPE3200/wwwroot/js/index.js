$(() => {
    symptom();
})


const symptom = () => {
    $.get("kalkulator/getSymptomer", symptomList => {
        format(symptomList);
    })
}

const format = symptomList => {
    let ut = `<select class="form-control form-control-lg" id="symptom" onchange="getSymptom()" ><option disabled selected value>Choose symptomes</option>`;
    for (let s of symptomList) {
        //has to have '' to embrace value when there is space in value!!!!!
        ut += `<option value='${s.symptomId}${s.symptomNavn}'>${s.symptomNavn}</option>`;
        console.log(s.symptomNavn);
    }
    ut += "</select>";

    $("#kalkulator").html(ut);
}

let sList = "";
let symptomArray = [];
const getSymptom = () => {
    let symptom = $("#symptom").val().substring(1);
    sList += symptom + "\n";
    $("#symptomList").html(sList);
    symptomArray.push(symptom);
}

let dList = "";
const diagnose = () => {
    console.log(symptomArray);
    $.post("kalkulator/searchDiagnoser", $.param({ symptomArray }, true), diagnoser => {
        if (diagnoser.length == 0) {
            $("#diagnoseList").html("fant ikke noen diagnosis...");
        }
        for (let d of diagnoser) {
            dList += d.diagnoseNavn + " - " + d.description + "\n";
            $("#diagnoseList").html(dList);
        }
    })
    dList = "";
    sList = "";
    symptomArray = [];
    $("#symptom")[0].selectedIndex = 0;
}