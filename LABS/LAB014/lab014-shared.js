var conexoes = 0;

self.addEventListener("connect", function (e) {
    var port = e.ports[0];
    conexoes++;

    port.onmessage = function (e) {
        port.postMessage(new Date().toLocaleTimeString());
    };

    port.start();

    setIntervalsetInterval(function () {
        resultadoHora();
    }, 1000);
})