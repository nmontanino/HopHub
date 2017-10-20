// Write your JavaScript code.
'use strict';

function getBeers(query) {
    $.ajax({
        url: "/home/getbeer?beer=" + query,
        success: function (response) {
            console.log(response);
            $('.results').empty();

            let beers = response.data
            // TODO: Paginate results to display 10 results per page. 50 results max.
            if (typeof(beers) != 'undefined') {
                $('.results').append(`<i>Displaying ${beers.length} results.</i><br>`);
                $('.results').append("<br>");
            } else {
                $('.results').append("<i>No search results.</i>"); 
            }

            let i = 0;
            for (i; i < beers.length; i++) {

                let name = beers[i].name;
                let styleName = beers[i].style.name;
                let ABV = beers[i].abv;
                let description = beers[i].description;
                let styleDesc = beers[i].style.description;
                let beerId = beers[i].id;

                $('.results').append(`<b><a href="/Beer?id=${beerId}">${name}</a></b><br>`);
                $('.results').append(`${styleName}<br>`);
                $('.results').append(`<a href="${beers[i].breweries[0].website}">${beers[i].breweries[0].name}</a><br>`)

                if (typeof (ABV) !== 'undefined') {
                    $('.results').append(`ABV: ${ABV}%<br>`);
                }

                $('.results').append("<br>");

                if (typeof (description) !== 'undefined') {
                    $('.results').append(`${description}<br>`);
                    $('.results').append("<br>");
                }

                $('.results').append(`Style Description: ${styleDesc}<br>`);
                $('.results').append("<br><br>");
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
