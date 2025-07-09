// wwwroot/site.js

function mostrarModalPdfDesdeBytes(dataUrl) {
    document.getElementById('iframePdf').src = dataUrl;
    const modal = new bootstrap.Modal(document.getElementById('pdfModal'));
    modal.show();
}


window.mostrarModalPdfDesdeBytes = function (pdfUrl) {
    const iframe = document.getElementById('iframePdf');
    iframe.src = pdfUrl;

    // Si usas Bootstrap 5
    const modal = new bootstrap.Modal(document.getElementById('pdfModal'));
    modal.show();
};
