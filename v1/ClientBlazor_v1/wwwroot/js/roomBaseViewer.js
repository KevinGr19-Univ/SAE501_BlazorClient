export function drawBaseOnCanvas(canvasId, points) {
    const canvasResolutionFactor = 4;
    const bgColor = "black";
    const circleColor = "dodgerblue";
    const circleRadius = 7 * canvasResolutionFactor;
    const edgeColor = "blue";
    const edgeThickness = 2 * canvasResolutionFactor;
    const font = `${14 * canvasResolutionFactor}px Helvetica`;
    const fontColor = "white";

    let canvas = document.querySelector(`#${canvasId}`);
    if (!canvas) return;

    let ctx = canvas.getContext("2d");
    canvas.width = canvas.clientWidth * canvasResolutionFactor;
    canvas.height = canvas.clientHeight * canvasResolutionFactor;

    ctx.clearRect(0, 0, canvas.width, canvas.height);
    ctx.fillStyle = bgColor;
    ctx.fillRect(0, 0, canvas.width, canvas.height);

    if (points.length == 0) return;

    let padding = 10 * canvasResolutionFactor;
    let totalWidth = canvas.width - padding * 2;
    let totalHeight = canvas.height - padding * 2;

    let minX = null, minY = null, maxX = null, maxY = null;
    for (let point of points) {
        if (minX === null || minX > point.x) minX = point.x;
        if (minY === null || minY > point.y) minY = point.y;
        if (maxX === null || maxX < point.x) maxX = point.x;
        if (maxY === null || maxY < point.y) maxY = point.y;
    }

    let diffX = maxX - minX;
    let diffY = maxY - minY;

    let midX = minX + diffX / 2;
    let midY = minY + diffY / 2;

    // Scaling
    let scaleX = diffX ? totalWidth / diffX : null;
    let scaleY = diffY ? totalHeight / diffY : null;

    let scale = 1;
    if (scaleX && scaleY) scale = Math.min(scaleX, scaleY);
    else if (scaleX) scale = scaleX;
    else if (scaleY) scale = scaleY;

    let scaledPoints = points.map(point => ({
        x: (point.x - midX) * scale + (totalWidth / 2 + padding),
        y: (midY - point.y) * scale + (totalHeight / 2 + padding),
    }));

    // Edges
    ctx.strokeStyle = edgeColor;
    ctx.lineWidth = edgeThickness;
    scaledPoints.forEach((point, i) => {
        let nextPoint = scaledPoints[(i + 1) % scaledPoints.length];
        ctx.beginPath();
        ctx.moveTo(point.x, point.y);
        ctx.lineTo(nextPoint.x, nextPoint.y);
        ctx.stroke();
    });

    // Points
    ctx.textAlign = "center";
    ctx.textBaseline = "middle";
    scaledPoints.forEach((point, i) => {
        ctx.fillStyle = circleColor;
        ctx.beginPath();
        ctx.arc(point.x, point.y, circleRadius, 0, Math.PI * 2);
        ctx.fill();

        ctx.fillStyle = fontColor;
        ctx.font = font;
        ctx.beginPath();
        ctx.fillText(`${i + 1}`, point.x, point.y);
    });
}