// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#District').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/DistrictHts/CreateJson",
                type: "GET",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.District, value: item.District };
                    }))
                }
            })

        },
        messages: {
            noResults:"",results:""
        }
    });
})

document.getElementById('nclientcode').focus();


$(document).ready(function () {
    $('#ctcno').mask('00-00-0000-000000');

});

