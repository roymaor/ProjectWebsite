// JavaScript source code
function checkName(text) {
    var isOk = checkLength(text);
    isOk = isOk && Ivrit(text);
    isOk = isOk && !NoSofit(text);

    return isOk;

}

function checkNumber() {  
    var isOk = checkLength(text) && stringIsHebrew(text) && NoSofit(text);
    return isOk;

}

function checkSizess(num) {
    if (num > 31 && num < 46) {
        return true;
    }
    else
        return false;


}
function areaCodeCheckBool(num) {
    if (num.length !== 3) {
        return false;
    }
    else {
        if (num[0] !== '0' || num[1] !== '5')
            return false;
        if (!(num[2] >= '0' && num[2] <= '9'))
            return false;
    }
    return true;
}

function checkLength(text) {
    //alert("Check Length - " + (text.length > 2));
    return text.length > 2;
}

function stringIsHebrew(str) {
    var index;               
    for (index = str.length - 1; index >= 0; --index) {
        if (Hebrew.indexOf(str.substring(index, index + 1)) < 0) {
            return false; 
        }
    }
    return true;
}
function Ivrit(args) {
    //var isOk = true;
    //var letters = /([à-ú])/;  // /([à-ú '"_])/;
    //for (var i = 0; i < args.length && isOk; i++) {
    //    if (!letters.test(args[i]))
    //        isOk = false;
    //}
    //return isOk;

    var isOk = true;
    var i;

    for (i = 0; i < args.length; i++) {
        //alert(i + " - " + args[i] + " - " + (args[i] < 'à' || args[i] > 'ú'));
        if (args[i] < 'à' || args[i] > 'ú') {
            isOk = false;
            //alert((args[i] < 'à' || args[i] > 'ú') + " - " + isOk);
        }
    }

    //alert("IVRIT - " + isOk);

    return isOk;
}


// Important to put "Ivrit" Function before "NameLength" Function, Returns true if there is hebrew.

function IvritandNumbers(args) {
    var isOk = true;
    var letters = /([à-ú '"_0-9])/;
    for (var i = 0; i < args.length && isOk; i++) {
        if (!letters.test(args[i]))
            isOk = false;
    }
    return isOk;
}

// Important to put "IvritandNumbers" Function before "NameLength" Function, Returns true if there is hebrew.





function NoSofit(text) {
    var isOk = true;

    if (text.charAt(0) === 'õ' || text.charAt(0) === 'ó' || text.charAt(0) === 'í' || text.charAt(0) === 'ê' || text.charAt(0) === 'ï')
        isOk = true;
    else
        isOk = false;

    //alert("NoSofit - " + isOk);

    return isOk;
}

// Important to put "NoSofit" Function Before "Ivrit" Function, Returns true if there is Sofit.


function EnglishAndNumbers(args) {
    var isOk = true;
    var letters = /(^\w+$)/;
    for (var i = 0; i < args.length && isOk; i++) {
        if (!letters.test(args[i]))
            isOk = false;
    }
    return isOk;
}
//Returns true if there is only English or Numbers.


function English(args) {
    var letters = /([a-zA-Z])/;
    return letters.test(args);
}
//Returns true if there is only English.


function Numbers(args) {
    var isOk = true;
    var letters = /([0-9])/;
    for (var i = 0; i < args.length && isOk; i++) {
        if (!letters.test(args[i]))
            isOk = false;
    }
    return isOk;
}








function txtPhoneCheck(source, args) {

    if (!PhoneLength(args.Value)) {
        args.IsValid = false;
    }
    else if (!PhoneNumbers(args.Value)) {
        args.IsValid = false;
    }
    else if (args.Value.charAt(0) < 2) {
        args.IsValid = false;
    }
}


function txtIdCheck(source, args) {
    var text = args.Value;
    if (text.length > 9)
        args.IsValid = false;
    else {
        var sum = 0;
        var number = 0;
        var id = "";
        var digits = 0;
        var asrot = 0;

        for (var j = 0; j < 9 - text.length; j++) {
            id += '0';
        }
        id += text;
        for (var i = 0; i < id.length; i++) {
            number = parseInt(id[i]);
            if (i % 2 === 0) {
                sum += number;
            }
            else {
                if (number * 2 >= 10) {
                    number = number * 2;
                    digits = Math.floor(number % 10);
                    asrot = Math.floor(number / 10);
                    sum += (asrot + digits);
                }
                else {
                    sum += number * 2;
                }
            }
        }
    }
    if (sum % 10 !== 0)
        args.IsValid = false;
}




function txtBirthdayCheck(source, args) {
    var birthday = new Date(args.Value);
    var thisyear = new Date();
    if (birthday.toString() === "Invalid Date")
        args.IsValid = false;
    else {
        var currectYear = thisyear.getFullYear();
        var year = birthday.getFullYear();
        if ((currectYear - year) < 18 || (currectYear - year) > 90)
            args.IsValid = false;
    }
}
function PasswordLength(args) {
    return args.length < 8;
}
function PhoneLength(args) {
    return args.length === 7;
}





