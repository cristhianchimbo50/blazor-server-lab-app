window.imprimirTicket = function () {
    const contenido = document.getElementById("ticketContent").innerHTML;
    const ventana = window.open('', '_blank', 'width=300,height=600');
    ventana.document.write(`
        <html>
            <head><title>Ticket</title></head>
            <body onload="window.print(); window.close();" style="font-family: monospace;">
                ${contenido}
            </body>
        </html>
    `);
    ventana.document.close();
};
