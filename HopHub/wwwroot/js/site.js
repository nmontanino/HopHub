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

                //if (typeof abv !== 'undefined') { avb = null }
                //if (typeof description !== 'undefined') { description = null }
                
                //$('.results').html(`<div class="list-group"><a href="/Beer?id=${beerId}" class="list-group-item">
                //    <h4 class="list-group-item-heading">${name}</h4>
                //    <h4 class="list-group-item-heading">${styleName}</h4>
                //    <h4 class="list-group-item-heading">${beers[i].breweries[0].name}</h4>
                //    <h4 class="list-group-item-heading">ABV: ${abv}%</h4>
                //    <p class="list-group-item-text">${description}</p>
                //    <p class="list-group-item-text">Style Description: ${styleDesc}</p></a></div>`);


                $('.results').append(`<b><a href="/Beer?id=${beerId}">${name}</a></b><br>`);
                $('.results').append(`${styleName}<br>`);
                $('.results').append(`${beers[i].breweries[0].name}<br>`);

                if (typeof abv !== 'undefined') {
                    $('.results').append(`ABV: ${abv}%<br>`);
                }

                $('.results').append("<br>");

                if (typeof description !== 'undefined') {
                    $('.results').append(`${description}<br>`);
                    $('.results').append("<br>");
                }

                $('.results').append(`Style Description: ${styleDesc}<br>`);
                $('.results').append("<hr>");
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
            $('.top').empty();

            let beer = response.data;
            let abv = beer.abv;
            let description = beer.description;
            let website = beer.breweries[0].website;
            let breweryDesc = beer.breweries[0].description;

            let image = beer.labels;

            // TODO: Put image back in once layout is fixed

            //if (typeof image !== 'undefined') {
            //    $('.info').append(`<img src="${image.medium}">`);
            //}

            $('.top').append(`<h4>${beer.name}</h4>`);
            $('.top').append(`<h4>${beer.style.name}</h4>`);
            $('.top').append(`<h4>${beer.breweries[0].name}</a></h4>`);

            if (typeof abv !== 'undefined') {
                $('.top').append(`<h4>ABV: ${abv}%</h4>`);
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
            }
            $('.info').append(`<p><a href="${website}">Brewery Website</a></p>`);
            $('.info').append(`<br>`);
            $('.info').append(`<a href="/Entry/Add?id=${beer.id}&name=${beer.name}" role="button" class="btn btn-success">Add ${beer.name} To Your Log</a>`);
        }
    });
}

$(document).ready(function () {

    $.urlParam = function (name) {
        var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
        if (results === null) {
            return null;
        }
        else {
            return decodeURI(results[1]) || 0;
        }
    }
    if ($('.results').length) {
        getBeers($.urlParam('beer'));
    }
    if ($('.info').length) {
        singleBeer($.urlParam('id'));
    }
});
