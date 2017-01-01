(function (servicoDeComentarios, $) {

    servicoDeComentarios.adicionarComentario = function (idDoPost, comentario, antesDeExecutar, sucesso, erro) {
        $.ajax({
            url: '/Post/Comentar',
            data: { idDoPost, conteudo: comentario },
            type: "POST",
            beforeSend: function () {
                antesDeExecutar();
            },
            success: function (retorno) {
                sucesso(retorno);
            },
            error: function (retorno) {
                erro(retorno);
            }
        });
    }

})(window.servicoDeComentarios = window.servicoDeComentarios || {}, jQuery);