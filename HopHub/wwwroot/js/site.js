// Write your JavaScript code.
'use strict';

function getBeers(query) {
    $.ajax({
        url: "/home/getbeer?beer=" + query,
        success: function (response) {
            console.log(response);
            $('.results').empty();

            let beers = response.data
            let i = 0
            for (i; i < 20; i++) {
                $('.results').append(`<b>${beers[i].name}</b>`);
                $('.results').append("<br>");
                $('.results').append(`${beers[i].style.name}`);
                $('.results').append("<br>");
                $('.results').append(`ABV: ${beers[i].abv}%`);
                $('.results').append("<br><br>");
                $('.results').append(`${beers[i].description}`);
                $('.results').append("<br><br>");
                $('.results').append(`Style Description: ${beers[i].style.description}`);
                $('.results').append("<br><br><br>");
            }
        }
    });
}

$(document).ready(function () {

    $('.submit_beer').click(function () {
        let beer = $('input.beer').val();
        console.log(beer);
        getBeers(beer);
    });

});
