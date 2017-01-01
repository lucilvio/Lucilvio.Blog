(function ($) {
    $(document).on("ready", function () {
        $(".adicionar-comentario").on("click", function () {
            var post = $(this).parents("div[class='post']");

            var idDoPost = post.find("#idDoPost").val();
            var comentario = post.find("#comentario");

            if (!comentario.valid()) {
                return false;
            }

            var listaDeComentarios = post.find("#lista-de-comentarios");
            var botaoComentar = $(this);

            servicoDeComentarios.adicionarComentario(idDoPost, comentario.val(),
                function () {
                    botaoComentar.attr("disabled", "disabled");
                },
                function (retorno) {
                    $(comentario).val("");

                    botaoComentar.removeAttr("disabled");
                    $(listaDeComentarios).append("<li style='font-size: small'>" + retorno.comentario + "<span style='font-size: x-small'> <i class='glyphicon glyphicon-time'></i> "
                        + retorno.data + " </span> </li>");
                },
                function (erro) {
                    alert("Erro ao fazer o comentário.");
                    botaoComentar.removeAttr("disabled");
                }
            );
        });
    });
})(jQuery);