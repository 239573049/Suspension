function addEventListener(key,DotNetHelper,name){
    document.addEventListener(key,async () => {
        await DotNetHelper.invokeMethodAsync(name);
    })
}

function onScroll(id, DotNetHelper, name) {
    var dom = document.getElementById(id);
    dom.addEventListener('scroll', async function (e) {
        const scrollTop = dom.scrollTop;
        await DotNetHelper.invokeMethodAsync(name,scrollTop);
    });
}

function scrollToBottom(id,value) {
    var element = document.getElementById(id);
    if(!element)
        return
    var scrollHeight = element.scrollHeight;
    var scrollTop = element.scrollTop;

    var scrollCount = 0;
    var scrollMargin;

    function scroll() {
        // 计算滚动条，当滚动条位置位于最下面的高度的百分之三十则自动滚动。
        if ((element.scrollTop + element.clientHeight + (80) > element.scrollHeight) || value) {
            scrollMargin = scrollHeight - scrollTop;
            element.scrollTop += scrollMargin / 20;
            scrollCount++;
            if (scrollCount < 30) {
                requestAnimationFrame(scroll);
            }
        }
    }

    scroll();
}

function  scrollBottom(id){
    var element = document.getElementById(id);
    if(element)
        element.scrollTop = element.scrollHeight;
}

export {
    addEventListener,
    onScroll,
    scrollToBottom,
    scrollBottom
}