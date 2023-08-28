const _modeloSedeOlimpica = {
    i_SedeId: 0,
    v_Nombre: "",
    i_NumeroComplejo: 0,
    d_PresupuestoAproximado: 0
}

function MostrarSedeOlimpica() {

    fetch("/Home/listaSedesOlimpicas")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {
            if (responseJson.length > 0) {

                $("#tablaSedeOlimpica tbody").html("");


                responseJson.forEach((sedeolimpica) => {
                    $("#tablaSedeOlimpica tbody").append(
                        $("<tr>").append(
                            $("<td>").text(sedeolimpica.v_Nombre),
                            $("<td>").text(sedeolimpica.i_NumeroComplejo),
                            $("<td>").text(sedeolimpica.d_PresupuestoAproximado),
                            $("<td>").append(
                                $("<button>").addClass("btn btn-primary btn-sm boton-editar-sedeolimpica").text("Editar").data("dataSedeOlimpica", sedeolimpica),
                                $("<button>").addClass("btn btn-danger btn-sm ms-2 boton-eliminar-sedeolimpica").text("Eliminar").data("dataSedeOlimpica", sedeolimpica),
                            )
                        )
                    )
                })

            }


        })

}

//Cuando toda la pagina ha sido cargada
document.addEventListener("DOMContentLoaded", function () {

    MostrarSedeOlimpica();

    //fetch("/Home/listaSedesOlimpicas")
    //    .then(response => {
    //        return response.ok ? response.json() : Promise.reject(response)
    //    })
    //    .then(responseJson => {

    //        if (responseJson.length > 0) {
    //            responseJson.forEach((item) => {

    //                $("#cboSede").append(
    //                    $("<option>").val(item.i_SedeId).text(item.v_Nombre)
    //                )

    //            })
    //        }

    //    })


}, false)

function MostrarModal() {
    $("#txtNombre").val(_modeloSedeOlimpica.v_Nombre);
    $("#txtNumeroComplejo").val(_modeloSedeOlimpica.i_NumeroComplejo);
    $("#txtPresupuestoAproximado").val(_modeloSedeOlimpica.d_PresupuestoAproximado);
    $("#modalSedeOlimpica").modal("show");
}

$(document).on("click", ".boton-nuevo-sedeolimpica", function () {

    _modeloSedeOlimpica.i_SedeId = 0;
    _modeloSedeOlimpica.v_Nombre = ""
    _modeloSedeOlimpica.i_NumeroComplejo = 0;
    _modeloSedeOlimpica.d_PresupuestoAproximado = 0;

    MostrarModal();
});

$(document).on("click", ".boton-editar-sedeolimpica", function () {
    const _sedeOlimpica = $(this).data("dataSedeOlimpica");

    _modeloSedeOlimpica.i_SedeId = _sedeOlimpica.i_SedeId;
    _modeloSedeOlimpica.v_Nombre = _sedeOlimpica.v_Nombre;
    _modeloSedeOlimpica.i_NumeroComplejo = _sedeOlimpica.i_NumeroComplejo;
    _modeloSedeOlimpica.d_PresupuestoAproximado = _sedeOlimpica.d_PresupuestoAproximado;

    MostrarModal();
});

$(document).on("click", ".boton-guardar-cambios-sedeolimpica", function () {
    const modelo =
    {
        i_SedeId: _modeloSedeOlimpica.i_SedeId,
        v_Nombre: $("#txtNombre").val(),
        i_NumeroComplejo: $("#txtNumeroComplejo").val(),
        d_PresupuestoAproximado: $("#txtPresupuestoAproximado").val(),
    }

    if (_modeloSedeOlimpica.i_SedeId == 0) {

        fetch("/Home/guardarSedesOlimpicas", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalSedeOlimpica").modal("hide");
                    Swal.fire("Listo!", "La Sede Olimpica fue creado", "success");
                    MostrarSedeOlimpica();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo crear", "error");
            })

    } else {

        fetch("/Home/editarSedesOlimpicas", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalSedeOlimpica").modal("hide");
                    Swal.fire("Listo!", "La Sede Olimpica fue actualizado", "success");
                    MostrarSedeOlimpica();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo actualizar", "error");
            })

    }


});


$(document).on("click", ".boton-eliminar-sedeolimpica", function () {
    const _sedeOlimpica = $(this).data("dataSedeOlimpica");

    Swal.fire({
        title: "Esta seguro?",
        text: `Eliminar Sede Olimpica "${_sedeOlimpica.v_Nombre}"`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, volver"
    }).then((result) => {

        if (result.isConfirmed) {

            fetch(`/Home/eliminarSedesOlimpicas?i_SedeId=${_sedeOlimpica.i_SedeId}`, {
                method: "DELETE"
            })
                .then(response => {
                    return response.ok ? response.json() : Promise.reject(response)
                })
                .then(responseJson => {

                    if (responseJson.valor) {
                        Swal.fire("Listo!", "La Sede Olimpica fue elminado", "success");
                        MostrarSedeOlimpica();
                    }
                    else
                        Swal.fire("Lo sentimos", "No se puedo eliminar", "error");
                })

        }



    })
});