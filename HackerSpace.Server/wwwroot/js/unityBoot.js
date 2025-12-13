 window.unityBoot = {
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
            warningBanner.appendChild(div);
            setTimeout(() => warningBanner.removeChild(div), 5000);
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

        document.body.appendChild(script);
    },

    stop: async function () {
        if (window._unityInstance) {
            await window._unityInstance.Quit();
            window._unityInstance = null;
        }
    }
};
