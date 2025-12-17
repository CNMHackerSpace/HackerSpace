window.godotHost = window.godotHost || {
    enterFullscreen: function (iframe) {
        const el = iframe instanceof HTMLElement ? iframe : document.querySelector(".godot-frame");
        if (!el) return;

        const fn = el.requestFullscreen || el.webkitRequestFullscreen || el.msRequestFullscreen;
        if (fn) fn.call(el);
    },

    stop: function (iframe) {
        // Exit fullscreen if the iframe (or anything) is currently fullscreen
        try {
            if (document.fullscreenElement) document.exitFullscreen();
            else if (document.webkitFullscreenElement) document.webkitExitFullscreen();
        } catch { }

        // Hard-stop the iframe content so it doesn't keep fetching / painting
        try {
            const el = iframe instanceof HTMLElement ? iframe : document.querySelector(".godot-frame");
            if (el) el.src = "about:blank";
        } catch { }
    }
};
