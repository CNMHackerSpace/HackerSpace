window.unityBoot = {
    start: function (buildUrl) {
        const canvas = document.querySelector("#unity-canvas");
        if (!canvas) return;

        // Always start fresh
        window._unityInstance = null;

        const script = document.createElement("script");
        script.src = buildUrl + "/Build.loader.js";
        script.onload = () => {
            createUnityInstance(canvas, {
                dataUrl: buildUrl + "/Build.data.unityweb",
                frameworkUrl: buildUrl + "/Build.framework.js.unityweb",
                codeUrl: buildUrl + "/Build.wasm.unityweb",
                streamingAssetsUrl: "StreamingAssets",
                companyName: "CNM Hackerspace",
                productName: "SuncatAdventures",
                productVersion: "0.1"
            });
        };

        document.body.appendChild(script);
    },

    stop: function () {
        // DO NOTHING INTENTIONALLY
        // Blazor owns the DOM, not Unity
        window._unityInstance = null;
    }
};
