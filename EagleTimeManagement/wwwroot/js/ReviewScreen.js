var ddl = document.querySelector('#DateRange');

ddl.addEventListener('change', function () {
    var span    = document.querySelector('#display-ddl')
    var ddlText = ddl.options[ddl.selectedIndex].text;

    if (ddlText === 'Select Date Range') {
        span.innerHTML = '';
    }
    else {
        span.innerHTML = ddlText;
    }
})
