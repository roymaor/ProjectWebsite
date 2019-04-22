// JavaScript source code
function checkName(text) {
    var isOk = checkLength(text) && stringIsHebrew(text) && firstLetter(text);


}
function checkLength(text) {
    return text.length > 2;
}
function stringIsHebrew(str) {
    var index;               
    for (index = str.lenght - 1; index >= 0; --index) {
        if (Hebrew.indexOf(str.substring(index, index + 1)) < 0) {
            return false;
        }
    }
}
return true;
} 
function firstLetter(str) {
    if (str.charAt(0) === "ï")
        return false;
    if (str.charAt(0) === "õ")
        return false;
    if (str.charAt(0) === "ó")
        return false;
    if (str.charAt(0) === "ê")
        return false;
    return true;

}