function resultadoHora() {
    postMessage(new Date().toLocaleTimeString());
}

setInterval(function () {
    resultadoHora();
}, 1000);

resultadoHora();