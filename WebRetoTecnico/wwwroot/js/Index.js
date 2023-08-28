const _modeloComplejoDeportivo = {
    i_ComplejoId: 0,
    i_SedeId: 0,
    v_TipoComplejo: "",
    v_Localizacion: "",
    v_JefeOrganizacion: "",
    v_AreaTotalOcupada: ""
}

function MostrarComplejoDeportivo() {

    fetch("/Home/listaComplejosDeportivos")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {
            if (responseJson.length > 0) {

                $("#tablaComplejoDeportivo tbody").html("");


                responseJson.forEach((complejodeportivo) => {
                    $("#tablaComplejoDeportivo tbody").append(
                        $("<tr>").append(
                            $("<td>").text(complejodeportivo.v_TipoComplejo),
                            $("<td>").text(complejodeportivo.refSedeOlimpica.v_Nombre),
                            $("<td>").text(complejodeportivo.v_Localizacion),
                            $("<td>").text(complejodeportivo.v_JefeOrganizacion),
                            $("<td>").text(complejodeportivo.v_AreaTotalOcupada),
                            $("<td>").append(
                                $("<button>").addClass("btn btn-primary btn-sm boton-editar-complejodeportivo").text("Editar").data("dataComplejoDeportivo", complejodeportivo),
                                $("<button>").addClass("btn btn-danger btn-sm ms-2 boton-eliminar-complejodeportivo").text("Eliminar").data("dataComplejoDeportivo", complejodeportivo),
                            )
                        )
                    )
                })

            }


        })

}

//Cuando toda la pagina ha sido cargada
document.addEventListener("DOMContentLoaded", function () {

    MostrarComplejoDeportivo();

    fetch("/Home/listaSedesOlimpicas")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {

            if (responseJson.length > 0) {
                responseJson.forEach((item) => {

                    $("#cboSede").append(
                        $("<option>").val(item.i_SedeId).text(item.v_Nombre)
                    )

                })
            }

        })

    //$("#txtFechaContrato").datepicker({
    //    format: "dd/mm/yyyy",
    //    autoclose: true,
    //    todayHighlight: true
    //})


}, false)

function MostrarModal() {
    $("#txtTipoComplejo").val(_modeloComplejoDeportivo.v_TipoComplejo);
    $("#cboSede").val(_modeloComplejoDeportivo.i_SedeId == 0 ? $("#cboSede option:first").val() : _modeloComplejoDeportivo.i_SedeId);
    $("#txtLocalizacion").val(_modeloComplejoDeportivo.v_Localizacion);
    $("#txtJefeOrganizacion").val(_modeloComplejoDeportivo.v_JefeOrganizacion);
    $("#txtAreaTotalOcupada").val(_modeloComplejoDeportivo.v_AreaTotalOcupada);
    $("#modalComplejoDeportivo").modal("show");
}

$(document).on("click", ".boton-nuevo-complejodeportivo", function ()
{

    _modeloComplejoDeportivo.i_SedeId = 0;
    _modeloComplejoDeportivo.v_TipoComplejo = "";
    _modeloComplejoDeportivo.v_Localizacion = "";
    _modeloComplejoDeportivo.v_JefeOrganizacion = "";
    _modeloComplejoDeportivo.v_AreaTotalOcupada = "";

    MostrarModal();
});

$(document).on("click", ".boton-editar-complejodeportivo", function ()
{
    const _complejoDeportivo = $(this).data("dataComplejoDeportivo");

    _modeloComplejoDeportivo.i_ComplejoId = _complejoDeportivo.i_ComplejoId;
    _modeloComplejoDeportivo.i_SedeId = _complejoDeportivo.refSedeOlimpica.i_SedeId;
    _modeloComplejoDeportivo.v_TipoComplejo = _complejoDeportivo.v_TipoComplejo;
    _modeloComplejoDeportivo.v_Localizacion = _complejoDeportivo.v_Localizacion;
    _modeloComplejoDeportivo.v_AreaTotalOcupada = _complejoDeportivo.v_AreaTotalOcupada;
    _modeloComplejoDeportivo.v_JefeOrganizacion = _complejoDeportivo.v_JefeOrganizacion;

    MostrarModal();
});

$(document).on("click", ".boton-guardar-cambios-complejodeportivo", function () {
    const modelo =
    {
        i_ComplejoId: _modeloComplejoDeportivo.i_ComplejoId,
        refSedeOlimpica:
        {
            i_SedeId: $("#cboSede").val()
        },
        v_TipoComplejo: $("#txtTipoComplejo").val(),
        v_Localizacion: $("#txtLocalizacion").val(),
        v_JefeOrganizacion: $("#txtJefeOrganizacion").val(),
        v_AreaTotalOcupada: $("#txtAreaTotalOcupada").val()
    }

    if (_modeloComplejoDeportivo.i_ComplejoId == 0) {

        fetch("/Home/guardarComplejosDeportivos", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalComplejoDeportivo").modal("hide");
                    Swal.fire("Listo!", "El Complejo Deportivo fue creado", "success");
                    MostrarComplejoDeportivo();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo crear", "error");
            })

    } else {

        fetch("/Home/editarComplejosDeportivos", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalComplejoDeportivo").modal("hide");
                    Swal.fire("Listo!", "El Complejo Deportivo fue actualizado", "success");
                    MostrarComplejoDeportivo();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo actualizar", "error");
            })

    }


});


$(document).on("click", ".boton-eliminar-complejodeportivo", function () {
    const _complejoDeportivo = $(this).data("dataComplejoDeportivo");

    Swal.fire({
        title: "Esta seguro?",
        text: `Eliminar Complejo Deportivo "${_complejoDeportivo.v_TipoComplejo}"`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, volver"
    }).then((result) => {

        if (result.isConfirmed) {

            fetch(`/Home/eliminarComplejosDeportivos?i_ComplejoId=${_complejoDeportivo.i_ComplejoId}`, {
                method: "DELETE"
            })
                .then(response => {
                    return response.ok ? response.json() : Promise.reject(response)
                })
                .then(responseJson => {

                    if (responseJson.valor) {
                        Swal.fire("Listo!", "El Complejo Deportivo fue elminado", "success");
                        MostrarComplejoDeportivo();
                    }
                    else
                        Swal.fire("Lo sentimos", "No se puedo eliminar", "error");
                })

        }



    })
});