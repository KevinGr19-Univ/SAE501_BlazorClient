function isInside(x, y, rect){
    return x >= rect.left && x <= rect.right && y >= rect.top && y <= rect.bottom;
}

function clamp(value, min, max){
    return Math.min(Math.max(value, min), max);
}

class FWindowContainer{
    constructor(element){
        this.element = element;
        this.fwindows = [];

        this.selected = null;
        this.grabbing = false;
        this.grabbingPos = null;

        window.addEventListener("mouseup", this.onWindowMouseUp);
        window.addEventListener("mousemove", this.onWindowMouseMove);
        window.addEventListener("resize", this.onWindowResize);
    }

    addFWindow(title){
        let fwindow = new FWindow(title, this);

        fwindow.element.addEventListener("mousedown", (e) => this.onFWindowMouseDown(fwindow, e));
        fwindow.header.addEventListener("click", (e) => {
            if(!this.grabbing) fwindow.toggleContent();
        });

        this.fwindows.push(fwindow);
        fwindow.updatePosition();
        this.setSelected(fwindow);
    }

    setSelected(fwindow){
        if(this.selected == fwindow) return;

        if(this.selected){
            this.selected.element.classList.remove("selected");
        }

        this.selected = fwindow;
        this.selected.element.classList.add("selected");
        this.element.appendChild(fwindow.element);
    }

    setGrabbing(val){
        this.grabbing = val;
        this.element.classList.toggle("grabbing", this.grabbing);
    }

    onWindowMouseUp = (e) => {
        setTimeout(() => this.setGrabbing(false), 0);
        this.grabbingPos = null;
    }

    onWindowMouseMove = (e) => {
        if(!this.selected || !this.grabbingPos) return;
        if(!this.grabbing) this.setGrabbing(true);

        let rect = this.element.getBoundingClientRect();

        let x = (e.pageX - rect.left) / rect.width - this.grabbingPos.x;
        let y = (e.pageY - rect.top) / rect.height - this.grabbingPos.y;

        this.selected.position.x = x;
        this.selected.position.y = y;

        this.selected.updatePosition();
    }

    onWindowResize = (e) => {
        this.fwindows.forEach(fwindow => fwindow.updatePosition());
    }

    onFWindowMouseDown(fwindow, e){
        this.setSelected(fwindow);
        
        let fwindowRect = fwindow.header.getBoundingClientRect();
        if(isInside(e.pageX, e.pageY, fwindowRect)){
            let rect = this.element.getBoundingClientRect();
            this.grabbingPos = {
                x: (e.pageX - fwindowRect.left) / rect.width,
                y: (e.pageY - fwindowRect.top) / rect.height
            };
        }
    }
}

class FWindow{
    constructor(title, container){
        this.title = title;
        this.container = container;
        this.position = {
            x: 0,
            y: 0
        };

        this.build();
    }

    build(){
        this.element = document.querySelector(".fwindow.template").cloneNode(true);

        this.element.classList.remove("template");
        this.header = this.element.querySelector(".fwindow_header");
        this.content = this.element.querySelector(".fwindow_content");

        this.header.innerText = this.title;
    }

    toggleContent(){
        this.element.classList.toggle("collapsed");
    }

    updatePosition(){
        let containerRect = this.container.element.getBoundingClientRect();
        let rect = this.header.getBoundingClientRect();

        let w = rect.width / containerRect.width;
        let h = rect.height / containerRect.height;

        let x = clamp(this.position.x, 0, 1-w);
        let y = clamp(this.position.y, 0, 1-h);

        this.element.style.left = `${x*100}%`;
        this.element.style.top = `${y*100}%`;
    }
}

var fwindow_container = new FWindowContainer(document.querySelector("#fwindow_container"));
fwindow_container.addFWindow("Test")

function clickAddFWindow(){
    let title = document.querySelector("#fwindow_new_title").value;
    fwindow_container.addFWindow(title);
}