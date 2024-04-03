function inicializaFormularioApartados(urlObtenerArea) {
    $("#AreaId").change(async function () {
        const valorSeleccionado = $(this).val();
        console.log(valorSeleccionado);

    try {
        const respuesta = await fetch(urlObtenerArea, {
            method: 'POST',
            body: valorSeleccionado,
            headers: {
                'Content-Type': 'application/json'
            }
        });

        const json = await respuesta.json();
        console.log(json);


    } catch (error) {
        console.error("Error al analizar JSON:", error)
        }

    })
}