'use strict';

function getBeers(query) {
    $.ajax({
        url: "/home/getbeer?beer=" + query,
        success: function (response) {
            console.log(response);

            // TODO: Paginate results to display 10 results per page. 50 results max.

            $('.results').empty();
            let beers = response.data;

            if (typeof beers !== 'undefined') {
                $('.results').append(`<i>Displaying ${beers.length} results.</i><br>`);
                $('.results').append("<br>");
            } else {
                $('.results').append("<i>No search results.</i>"); 
            }

            let i = 0;
            for (i; i < beers.length; i++) {

                let name = beers[i].name;
                let styleName = beers[i].style.name;
                let abv = beers[i].abv;
                let description = beers[i].description;
                let styleDesc = beers[i].style.description;
                let beerId = beers[i].id;

                $('.results').append(`<b><a href="/Beer?id=${beerId}">${name}</a></b><br>`);
                $('.results').append(`${styleName}<br>`);
                $('.results').append(`<a href="${beers[i].breweries[0].website}">${beers[i].breweries[0].name}</a><br>`);

                if (typeof abv !== 'undefined') {
                    $('.results').append(`ABV: ${abv}%<br>`);
                }

                $('.results').append("<br>");

                if (typeof description !== 'undefined') {
                    $('.results').append(`${description}<br>`);
                    $('.results').append("<br>");
                }

                $('.results').append(`Style Description: ${styleDesc}<br>`);
                $('.results').append("<br><br>");
            }
        }
    });
}
function singleBeer(beerId) {
    $.ajax({
        url: "/beer/singlebeer?id=" + beerId,
        success: function (response) {
            console.log(response);

            $('.info').empty();

            let beer = response.data;
            let abv = beer.abv;
            let description = beer.description;
            let website = beer.breweries[0].website
            let breweryDesc = beer.breweries[0].description
            //let image = beer.labels.medium;

            //if (typeof image !== 'undefined') {
            //    $('.info').append(`<img src="${image}">`);
            //}

            $('.info').append(`<h3>${beer.name}</h3>`);
            $('.info').append(`<h4>${beer.style.name}</h4>`);
            $('.info').append(`<h4>${beer.breweries[0].name}</a></h4>`);

            if (typeof abv !== 'undefined') {
                $('.info').append(`<h4>ABV: ${abv}%</h4>`);
            }
            $('.info').append(`<br>`);

            if (typeof description !== 'undefined') {
                $('.info').append(`<p><b>Beer Description</b></p>`);
                $('.info').append(`<p>${description}</p><br>`);
            }
            $('.info').append(`<p><b>Style Description</b></p>`);
            $('.info').append(`<p>${beer.style.description}</p><br>`);

            if (typeof breweryDesc !== 'undefined') {
                $('.info').append(`<p><b>Brewery Description</b></p>`);
                $('.info').append(`<p>${breweryDesc}</p>`);
                $('.info').append(`<p><a href="${website}">Website</a></p>`);
            }
        }
    });
}

$(document).ready(function () {

    $('.submit_beer').click(function () {
        let beer = $('input.beer').val();
        getBeers(beer);
    });

    $.urlParam = function (name) {
        var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
        if (results == null) {
            return null;
        }
        else {
            return decodeURI(results[1]) || 0;
        }
    }

    if ($('.info').length) {
        singleBeer($.urlParam('id'));
    }
});
