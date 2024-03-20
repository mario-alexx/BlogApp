window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Operación Correcta", { timeOut: 10000 });
    }
    if (type === "error") {
        toastr.error(message, "Operación Fallida", { timeOut: 10000 });
    }
}

window.ShowSwal = (type, message) => {
    if (type === "success") {
        Swal.fire(
            'Success Notification',
            message,
            'success'
        );
    }
    if (type === "error") {
        Swal.fire(
            'Error Notification',
            message,
            'error'
        );
    }
}

function MostrarModalConfirmacionBorrado() {
    $('#modalConfirmacionBorrado').modal('show');
}

function OcultarModalConfirmacionBorrado() {
    $('#modalConfirmacionBorrado').modal('hide');
}