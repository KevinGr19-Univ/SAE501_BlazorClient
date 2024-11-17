document.querySelectorAll(".fwindow").forEach(fwindow => {
    let header = $(fwindow).find(".fwindow_header");
    $(header).on("click", () => $(fwindow).toggleClass("collapsed"));
    $(header).find(".close_btn").on("click", () => fwindow.remove());

    let content = $(fwindow).find(".fwindow_content");
    $(content).find(".collapse_box").each((i, box) => {
        $(box).find(".collapse_header").on("click", () => $(box).toggleClass("collapsed"));
    });
});