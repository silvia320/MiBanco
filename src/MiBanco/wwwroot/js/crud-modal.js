// CRUD con modal usando jQuery.
//  - Cualquier elemento con data-modal-url abre el modal y carga ahi (GET) el formulario que devuelve el servidor.
//  - Al enviar el formulario se hace POST por AJAX: si es valido se recarga el listado;
//    si hay errores de validacion (422) se muestra de nuevo el formulario con sus mensajes.
(function ($) {
    'use strict';

    var $modal = $('#crudModal');
    var $content = $modal.find('.modal-content');
    var modal = null;

    function getModal() {
        if (!modal) { modal = new bootstrap.Modal($modal[0]); }
        return modal;
    }

    function showError(message) {
        $content.find('.modal-error').remove();
        var $alert = $('<div class="alert alert-danger modal-error"></div>').text(message);
        $content.find('.modal-body').prepend($alert);
    }

    // Abrir el modal (crear, editar o eliminar)
    $(document).on('click', '[data-modal-url]', function (e) {
        e.preventDefault();
        var url = $(this).data('modal-url');

        $content.html('<div class="modal-body text-center p-5"><div class="spinner-border text-primary" role="status"></div></div>');
        getModal().show();

        $.get(url)
            .done(function (html) { $content.html(html); })
            .fail(function () { $content.html('<div class="modal-body text-danger">No se pudo cargar el formulario.</div>'); });
    });

    // Enviar el formulario del modal por AJAX
    $modal.on('submit', 'form', function (e) {
        e.preventDefault();
        var $form = $(this);
        var $submit = $form.find('[type=submit]').prop('disabled', true);

        $.ajax({ url: $form.attr('action'), type: 'POST', data: $form.serialize() })
            .done(function (res) {
                if (res && res.ok) {
                    getModal().hide();
                    window.location.reload();
                } else {
                    showError((res && res.message) || 'No se pudo completar la operación.');
                    $submit.prop('disabled', false);
                }
            })
            .fail(function (xhr) {
                if (xhr.status === 422) {
                    $content.html(xhr.responseText);   // formulario con los mensajes de validacion
                } else {
                    showError('Ocurrió un error inesperado.');
                    $submit.prop('disabled', false);
                }
            });
    });

    // Limpiar el contenido al cerrar
    $modal.on('hidden.bs.modal', function () { $content.empty(); });
})(jQuery);