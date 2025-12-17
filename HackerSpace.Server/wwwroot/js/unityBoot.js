window.unityBoot = {
    _bannerTimeouts: [],

    start: function (buildUrl) {

        const canvas = document.querySelector("#unity-canvas");
        if (!canvas) {
            console.warn("Unity canvas not found yet.");
            return;
        }

        if (window._unityInstance) {
            console.warn("Unity already running.");
            return;
        }

        const loadingBar = document.querySelector("#unity-loading-bar");
        const progressBarFull = document.querySelector("#unity-progress-bar-full");
        const fullscreenButton = document.querySelector("#unity-fullscreen-button");
        const warningBanner = document.querySelector("#unity-warning");

         function unityShowBanner(msg, type) {
             const div = document.createElement("div");
             div.innerHTML = msg;
             div.style = type === "error"
                 ? "background:red;padding:10px"
                 : "background:yellow;padding:10px";

             if (warningBanner) warningBanner.appendChild(div);

             const t = setTimeout(() => {
                 if (div.parentNode) div.parentNode.removeChild(div);
             }, 5000);

             window.unityBoot._bannerTimeouts.push(t);
         }

        const config = {
            dataUrl: buildUrl + "/Deployment.data.gz",
            frameworkUrl: buildUrl + "/Deployment.framework.js.gz",
            codeUrl: buildUrl + "/Deployment.wasm.gz",
            streamingAssetsUrl: "StreamingAssets",
            companyName: "CNM Hackerspace",
            productName: "SuncatAdventures",
            productVersion: "0.1",
            showBanner: unityShowBanner
        };

        loadingBar.style.display = "block";

        const script = document.createElement("script");
        script.src = buildUrl + "/Deployment.loader.js";
        script.onload = () => {
            createUnityInstance(canvas, config, progress => {
                progressBarFull.style.width = (progress * 100) + "%";
            }).then(instance => {
                window._unityInstance = instance;
                loadingBar.style.display = "none";
                fullscreenButton.onclick = () => instance.SetFullscreen(1);
            }).catch(alert);
        };

        window._unityLoaderScript?.remove();
        window._unityLoaderScript = script;
        document.body.appendChild(script);
    },

    stop: async function () {
        // cancel pending banner removals
        for (const t of window.unityBoot._bannerTimeouts) clearTimeout(t);
        window.unityBoot._bannerTimeouts = [];

        window._unityLoaderScript?.remove();
        window._unityLoaderScript = null;

        const inst = window._unityInstance;
        if (!inst) return;
        window._unityInstance = null;

        try { await inst.Quit(); }
        catch (e) { console.warn("Unity Quit failed:", e); }
    }
};
