$(() => {
    editTable();
})

let id = window.location.search.substring(1); // id = id=1
console.log(id.substring(3));

let ut = "";
const editTable = () => {
    let url = "kalkulator/getEnDiagnose?diagnose" + id; //has to have diagnose in url!!!!!!
    
    $.get(url, diagnose => {
        $("#diagnoseNavn").html("Diagnose: " + diagnose.diagnoseNavn);
        for (let navn of diagnose.symptomNavnene) {
            ut += `<input class="symptomCheckbox" type="checkbox" id=${navn} value=${navn} onchange="checkList()" checked>
                    <label for=${navn}>${navn}</label><br>`
        }
        $("#table").html(ut);
    })
}

let added = [];
let ut2 = "";
const addSymptom = () => {
    let inputValue = $("#inputSymptom").val();
    console.log(inputValue);
    if (inputValue !== "") {
        added.push(inputValue);
    }
    for (let item of added) {
        ut2 += `<input class="symptomCheckbox" type="checkbox" id=${item} value=${item} onchange="checkList()" checked>
                    <label for=${item}>${item}</label><br>`;
    }
    $("#table2").html(ut2);
    added = [];
    
    $("#inputSymptom").val("");
}


const checkList = () => {
    
}

const sendSymptomer = () => {
    let symptomList = [id.substring(3)];
    $("input[type='checkbox']:checked").each(function () {
        symptomList.push($(this).val());
    });
    console.log(symptomList);

    $.post("kalkulator/updateSymptomer", $.param({ symptomList }, true), OK => {
        if (OK) {
            $("#tilbakeMelding").html("symptoms have updated.");
        }
        else {
            $("#tilbakeMelding").html("something wrong with the database, please try later");
        }
    })
}

