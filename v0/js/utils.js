function clamp(value, min, max){
    return Math.min(Math.max(value, min), max);
}

//#region Drag & Drop Floating windows
let cursorDown = false;
let selectedFloatingWindow = $(".floating_window").get(0);

window.addEventListener("mousedown", (e) => cursorDown = true);
window.addEventListener("mouseup", (e) => cursorDown = false);

window.addEventListener("mousemove", (e) => {
    if(!cursorDown || !selectedFloatingWindow) return;

    let canvasRect = $("#canvas_render").get(0).getBoundingClientRect();
    let x = e.pageX - canvasRect.left;
    let y = e.pageY - canvasRect.top;

    selectedFloatingWindow.style.left = `${x}px`;
    selectedFloatingWindow.style.top = `${y}px`;
});
//#endregion