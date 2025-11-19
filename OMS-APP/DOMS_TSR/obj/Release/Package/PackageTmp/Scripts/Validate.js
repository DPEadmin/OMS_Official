function onlyNumber(inputtxt) {


    if (isNaN(inputtxt.value)) {
        alert("Must input numbers");
        inputtxt.value = "";
    }

}

function phonenumberMobile(inputtxt) {

    var phoneno = /^(\([0-9]{3}\)\s*|[0-9]{3}\-)[0-9]{3}-[0-9]{4}$/;

    if (inputtxt.value.match(phoneno)) {
    //    alert("inputtxt.value.match(phoneno)=" + inputtxt.value.match(phoneno));
        return true;
    }
    else {
        alert("ระบุหมายเลขไม่ครบ 10 หลัก");
        return false;
    }
}


function phonenumber(inputtxt) {
    var phoneno = /^[0]{1}[2-7]{1}[0-9]{7}$/;
    var text = inputtxt.value;
    if (inputtxt.value != "") {
        if (inputtxt.value.match(phoneno)) {
            return true;
        }
        else {
            alert("ระบุหมายเลขไม่ครบ 9 หลัก");
            inputtxt.value = "";
            inputtxt.focus();
            return false;
        }
    }
}