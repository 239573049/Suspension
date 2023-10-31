window.MasaBlazor.extendMarkdownIt = function (parser) {

    console.log(parser)
    const { md, scope } = parser;
    if (window.markdownitEmoji) {
        md.use(window.markdownitEmoji);
        
    }

    if (scope === "document") {
        md.renderer.rules.code_block = renderCode(md.renderer.rules.code_block, md.options);
        md.renderer.rules.fence = renderCode(md.renderer.rules.fence, md.options);
    }
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
<div style="position: relative">
	${origRendered}
	<button class="markdown-it-code-copy " onclick="copy('${stringToBase64(tokens[idx].content)}')" style="position: absolute; top: 7.5px; right: 6px; cursor: pointer; outline: none;" title="复制">
		<span style="font-size: 21px; opacity: 0.4;" class="mdi mdi-content-copy"></span>
	</button>
</div>
`;
    };
}

window.copy = (e)=>{
    navigator.clipboard.writeText(base64ToString(e))
}

function stringToBase64(str) {
    var base64 = btoa(str);
    return base64;
}

function base64ToString(base64) {
    var str = atob(base64);
    return str;
}