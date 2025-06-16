window.imprimirResultados = function (data) {
    const contenido = `
        <div style="font-family: Arial, sans-serif; padding: 20px; width: 100%; max-width: 800px; margin: auto;">
            <h2 style="text-align: center;">LABORATORIO CLÍNICO LA INMACULADA</h2>
            <p style="text-align: center;">Av. 20 de Diciembre y López de Galarza - Guano, Chimborazo</p>
            <hr/>
            <p><strong>N° Resultado:</strong> ${data.NumeroResultado}</p>
            <p><strong>Nombre:</strong> ${data.NombrePaciente}</p>
            <p><strong>Cédula:</strong> ${data.CedulaPaciente}</p>
            <p><strong>Fecha:</strong> ${new Date(data.FechaResultado).toLocaleDateString()}</p>
            <br/>
            <table style="width: 100%; border-collapse: collapse;" border="1">
                <thead>
                    <tr style="background-color: #f2f2f2;">
                        <th>Examen</th>
                        <th>Valor</th>
                        <th>Unidad</th>
                        <th>Valor Referencia</th>
                    </tr>
                </thead>
                <tbody>
                    ${data.Examenes.map(ex => `
                        <tr>
                            <td>${ex.NombreExamen}</td>
                            <td>${ex.Valor}</td>
                            <td>${ex.Unidad ?? ""}</td>
                            <td>${ex.ValorReferencia ?? ""}</td>
                        </tr>
                    `).join('')}
                </tbody>
            </table>
            <br/>
            <p><strong>Observaciones:</strong></p>
            <p>${data.Observaciones ?? "Sin observaciones."}</p>
        </div>
    `;

    const ventana = window.open('', '_blank', 'width=900,height=1200');
    ventana.document.write(`
        <html>
            <head><title>Resultado de Exámenes</title></head>
            <body style="padding: 20px;">${contenido}</body>
        </html>
    `);
    ventana.document.close();
};
