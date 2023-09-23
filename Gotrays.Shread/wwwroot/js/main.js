function init(id) {
    document.getElementById(id)
        .addEventListener("mousedown", async () => {
            await DotNet.invokeMethodAsync('Gotrays.Desktop', 'StartMove')

            document.addEventListener('mousemove', start)

            document.addEventListener('mouseup', stop)

            function start() {
                DotNet.invokeMethodAsync('Gotrays.Desktop', 'UpdateWindowPos')
            }

            function stop() {
                document.removeEventListener('mousemove', start)
                document.removeEventListener('mouseup', stop)
                DotNet.invokeMethodAsync('Gotrays.Desktop', 'StopMove')
            }
        })
}

function qrCode(id,value){

    const qr = new QRCode(document.getElementById(id), {
        width: 300,
        height: 300
    });

    qr.makeCode(value);
}

export {
    init,
    qrCode
}