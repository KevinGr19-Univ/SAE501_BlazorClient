let cursorDown = false;
let selectedFWindow = $(".floating_window").get(0);
selectedFWindow.intrinsicX = 10;
selectedFWindow.intrinsicY = 10;


function updateFWindowPosition(fwindow){
    let canvasRect = $("#canvas_render").get(0).getBoundingClientRect();
    let elementRect = selectedFWindow.getBoundingClientRect();
    
    let w = elementRect.width * 100 / canvasRect.width;
    let x = clamp(fwindow.intrinsicX, w/2, 100-w/2);
    fwindow.style.left = `${x}%`;
    
    let h = elementRect.height * 100 / canvasRect.height;
    let y = clamp(fwindow.intrinsicY, h/2, 100-h/2);
    fwindow.style.top = `${y}%`;
}

let cursorPosition = {x: null, y: null};
window.addEventListener("mousedown", (e) => {
    cursorDown = true;
    cursorPosition = {x: e.pageX, y: e.pageY};
});
window.addEventListener("mouseup", (e) => cursorDown = false);

window.addEventListener("mousemove", (e) => {
    if(!cursorDown || !selectedFWindow) return;

    let canvasRect = $("#canvas_render").get(0).getBoundingClientRect();
    let elementRect = selectedFWindow.getBoundingClientRect();

    let x = (e.pageX - canvasRect.left) / canvasRect.width;
    let y = (e.pageY - canvasRect.top) / canvasRect.height;

    x += (elementRect.width/canvasRect.width)/2;
    y += (elementRect.height/canvasRect.height)/2;

    selectedFWindow.intrinsicX = x*100;
    selectedFWindow.intrinsicY = y*100;

    updateFWindowPosition(selectedFWindow);
});

window.addEventListener("resize", (e) => {
    updateFWindowPosition(selectedFWindow);
});

updateFWindowPosition(selectedFWindow);