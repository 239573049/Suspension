window.MasaBlazor.extendMarkdownIt = function (parser) {

    const { md, scope } = parser;
    if (window.markdownitEmoji) {
        md.use(window.markdownitEmoji);
    }

    md.renderer.rules.code_block = renderCode(md.renderer.rules.code_block, md.options);
    md.renderer.rules.fence = renderCode(md.renderer.rules.fence, md.options);
}
function renderCode(origRule, options) {
    return (...args) => {
        const [tokens, idx] = args;
        const content = tokens[idx].content
            .replaceAll('"', '&quot;')
            .replaceAll("'", "&lt;");
        const origRendered = origRule(...args);

        if (content.length === 0)
            return origRendered;

        return `
<div style="position: relative;background-color:#d1d1d1;border-radius: 12px;">
	${origRendered}
	<button class="markdown-it-code-copy" onclick="copy('${base64Encode(tokens[idx].content)}')" style="position: absolute; top: 5px; right: 5px; cursor: pointer; outline: none;" title="复制">
		<span style="font-size: 21px; opacity: 0.4;" class="mdi mdi-content-copy"></span>
	</button>
</div>
`;
    };
}

window.copy = async (e) => {
    navigator.clipboard.writeText(base64Decode(e))
    await DotNet.invokeMethodAsync('Gotrays.Rcl', 'Notifications', "复制成功")
}

// Base64编码
function base64Encode(str) {
    str = window.encodeURIComponent(str);
    let base64Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
    let result = "";
    let i = 0;
    let chr1, chr2, chr3, enc1, enc2, enc3, enc4;

    while (i < str.length) {
        chr1 = str.charCodeAt(i++);
        chr2 = str.charCodeAt(i++);
        chr3 = str.charCodeAt(i++);

        enc1 = chr1 >> 2;
        enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
        enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
        enc4 = chr3 & 63;

        if (isNaN(chr2)) {
            enc3 = enc4 = 64;
        } else if (isNaN(chr3)) {
            enc4 = 64;
        }

        result +=
            base64Chars.charAt(enc1) +
            base64Chars.charAt(enc2) +
            base64Chars.charAt(enc3) +
            base64Chars.charAt(enc4);
    }

    return result;
}

// Base64解码
function base64Decode(str) {
    let base64Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
    let result = "";
    let i = 0;
    let chr1, chr2, chr3;
    let enc1, enc2, enc3, enc4;

    str = str.replace(/[^A-Za-z0-9+/=]/g, "");

    while (i < str.length) {
        enc1 = base64Chars.indexOf(str.charAt(i++));
        enc2 = base64Chars.indexOf(str.charAt(i++));
        enc3 = base64Chars.indexOf(str.charAt(i++));
        enc4 = base64Chars.indexOf(str.charAt(i++));

        chr1 = (enc1 << 2) | (enc2 >> 4);
        chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
        chr3 = ((enc3 & 3) << 6) | enc4;

        result += String.fromCharCode(chr1);

        if (enc3 !== 64) {
            result += String.fromCharCode(chr2);
        }
        if (enc4 !== 64) {
            result += String.fromCharCode(chr3);
        }
    }

    result = window.decodeURI(result)
    return result;
}