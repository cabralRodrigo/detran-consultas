(function ($) {
    'use strict';

    $(function () {
        var aulasElemento = $('#aulas');

        if (!aulasElemento.length)
            return;

        var aulas = $('#aulas').DataTable({
            pageLength: 50,
            columns: [{ orderable: false, className: 'aula-detalhe' }, null, null, null, null],
            order: [[1, 'asc'], [2, 'asc']]
        });

        $('#aulas tbody').on('click', 'td.aula-detalhe', function () {
            var tr = $(this).closest('tr');
            var row = aulas.row(tr);

            var icon = $(this).children('i');

            if (row.child.isShown()) {
                row.child.hide();
                icon.removeClass('fa-minus').addClass('fa-plus');
            }
            else {
                var detalhes = tr.find('.detalhes');
                icon.removeClass('fa-plus').addClass('fa-minus');
                row.child(detalhes.html()).show();
            }
        });
    });
})(jQuery);