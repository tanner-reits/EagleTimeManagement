﻿$(document).ready(function () {
    var ddl     = document.querySelector('#DateRange');
    var span    = document.querySelector('#display-range');

    ddl.addEventListener('change', function () {
        var ddlText    = ddl.options[ddl.selectedIndex].text
        var timePeriod = ddl.options[ddl.selectedIndex].value;

        if (ddlText == 'Select Date Range') {
            span.innerHTML = '';
            $('#data-table').hide();
        }
        else {
            span.innerHTML = 'Showing entries for ' + ddlText;
            $('#data-table').show();

            $.ajax({
                type: "GET",
                url: "/Review/loadTable",
                data: { id: timePeriod },
                success: function (data) {
                    $('#data-table').html(data);
                },
                error: function () {
                    console.log('Error');
                }
            });
        }
    })
})