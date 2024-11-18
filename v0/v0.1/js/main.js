function setupFWindows(){
    $(".collapse_box_header").on("click", (e) => {
        $(e.currentTarget.parentElement).toggleClass("collapsed");
    });

    $(".fwindow .close_btn").on("click", (e) => {
        $(e.currentTarget).parents(".fwindow").remove();
    });
}

setupFWindows();
var scene = new Scene($("#renderer").get(0));