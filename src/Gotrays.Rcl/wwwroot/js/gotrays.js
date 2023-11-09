
function onScroll(id, DotNetHelper, name) {
    var dom = document.getElementById(id);
    dom.addEventListener('scroll', async function (e) {
        console.log(dom.scrollTop, dom.scrollTop === 0);
        if (dom.scrollTop === 0) {
            await DotNetHelper.invokeMethodAsync(name, dom.scrollTop);
        }
    });
}

function  scrollBottom(id){
    const element = document.getElementById(id);
    if(element)
        element.scrollTop = element.scrollHeight;
}

function copyText(text) {
    /* 获取要复制的内容 */
    /* 创建一个临时的textarea元素 */
    var tempInput = document.createElement("textarea");
    tempInput.value = text;
    document.body.appendChild(tempInput);

    /* 选择并复制文本 */
    tempInput.select();
    document.execCommand("copy");

    /* 删除临时元素 */
    document.body.removeChild(tempInput);

}

function qrCode(id, value) {

    const qr = new QRCode(document.getElementById(id), {
        width: 300,
        height: 300
    });

    qr.makeCode(value);
}
export {
    onScroll,
    scrollBottom,
    copyText,
    qrCode
}