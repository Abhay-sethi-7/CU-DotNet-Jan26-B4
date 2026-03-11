function checkSum(){

let n1 = document.getElementById("num1");
let n2 = document.getElementById("num2");
let result = document.getElementById("result");

let v1 = n1.value.trim();
let v2 = n2.value.trim();

if(v1=="" || v2==""){
    if(v1=="") n1.style.backgroundColor="red";
    if(v2=="") n2.style.backgroundColor="red";

    result.value="Error";
    result.style.backgroundColor="darkgoldenrod"; 
    return;
}

if(isNaN(v1) || isNaN(v2)){
    if(isNaN(v1)) n1.style.backgroundColor="yellow";
    if(isNaN(v2)) n2.style.backgroundColor="yellow";

    result.value="Error";
    result.style.backgroundColor="darkgoldenrod";
    return;
}

n1.style.backgroundColor="white";
n2.style.backgroundColor="white";
result.style.backgroundColor="white";

let sum = Number(v1) + Number(v2);
result.value = sum;

}