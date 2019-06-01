$(document).ready(function () {
    var ddl     = document.querySelector('#DateRange');
    var span    = document.querySelector('#display-range');

    ddl.addEventListener('change', function () {
        var ddlText = ddl.options[ddl.selectedIndex].text;

        if (ddlText == 'Select Date Range') {
            span.innerHTML = '';
        }
        else {
            span.innerHTML = 'Showing entries for ' + ddlText;
        }
    })
})