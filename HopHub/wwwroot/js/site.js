'use strict';

function getBeers(query, pageNum) {
    $.ajax({
        url: "/home/getbeer?beer=" + query + "&pageNum=" + pageNum,
        success: function (response) {
            console.log(response);

            $('.results').empty();
            let beers = response.data;
            let numResults = response.totalResults;

            if (typeof beers !== 'undefined') {
                $('.results').append(`<i>Displaying ${beers.length} out of ${numResults} results.</i><br>`);
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

            // Paginates results to display 50 results per page.
            $('.pagination').empty();
            var pages = response.numberOfPages;

            if (pages > 1) {
                for (var p = 0; p < pages; p++) {
                    $('.pagination').append(`<li><a href="?beer=${query}&pageNum=${p + 1}">${p + 1}</a></li>`);
                }
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
            $('.media-right').empty();

            let beer = response.data;
            let abv = beer.abv;
            let description = beer.description;
            let website = beer.breweries[0].website;
            let breweryDesc = beer.breweries[0].description;
            let image = beer.labels;

            if (typeof image !== 'undefined') {
                $('.media-right').wrapInner(`<a href="${image.large}"><img class="media-object" src="${image.medium}" height="128px"></a>`);
            } else {
                $('.media-right').wrapInner(`<img class="media-object" src="/images/noun_3235_cc-gry2-lg.svg" height="118px">`);
            }
            
            $('.top').append(`<h4>${beer.name}</h4>`);
            $('.top').append(`<h4>${beer.style.name}</h4>`);
            $('.top').append(`<h4>${beer.breweries[0].name}</h4>`);

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
        getBeers($.urlParam('beer'), $.urlParam('pageNum'));
    }
    if ($('.info').length) {
        singleBeer($.urlParam('id'));
    }
});
