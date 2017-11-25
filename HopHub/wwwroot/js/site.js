'use strict';

function notEmpty(value) {
    return typeof value !== 'undefined';
}

function getBeers(query, pageNum) {
    $.ajax({
        url: "/home/getbeer?beer=" + query + "&pageNum=" + pageNum,
        success: function (response) {
            console.log(response);

            $('.results').empty();
            $('.pager').empty();

            let beers = response.data;
            let numResults = response.totalResults;
            let pages = response.numberOfPages;
            let currentPage = response.currentPage;

            // Create pager if more than 50 results
            if (pages > 1) {
                if (currentPage > 1) {
                    $('.pager').append(`<li class="previous"><a href="?beer=${query}&pageNum=${currentPage - 1}">&larr; Previous</a></li>`);
                }
                $('.pager').append(`<li class="next"><a href="?beer=${query}&pageNum=${currentPage + 1}">Next &rarr;</a></li>`);
            }

            // Display number of results and page number
            if (notEmpty(beers)) {
                $('.results').append(`<p><i>Displaying ${beers.length} out of ${numResults} results. (Page ${currentPage} of ${pages})</i></p>`);
                $('.results').append("<br>");
            } else {
                $('.results').append("<i>No search results.</i>"); 
            }

            let i = 0;
            for (i; i < beers.length; i++) {

                let name = beers[i].name;
                let style = beers[i].style;
                let abv = beers[i].abv;
                let description = beers[i].description;
                let beerId = beers[i].id;
                let brewery = beers[i].breweries[0];

                // Use placeholder image if label not available
                let image = beers[i].labels;
                if (notEmpty(image)) {
                    $('.results').append(`<div class="pull-right"><img class="img-circle" src="${image.icon}"><div>`);
                } else {
                    $('.results').append(`<div class="pull-right"><img class="thumbnail" src="/images/noun_3235_cc-gry2-lg.svg" height="64px"><div>`);
                }

                // Link to individual beer page
                $('.results').append(`<b><a href="/Beer?id=${beerId}">${name}</a></b><br>`);

                if (notEmpty(style)) {
                    $('.results').append(`<b>${style.name}</b><br>`);
                }

                if (notEmpty(brewery)) {
                    $('.results').append(`<b>${brewery.name}</b><br>`);
                }

                if (notEmpty(abv)) {
                    $('.results').append(`<b>ABV: ${abv}%</b><br>`);
                }

                $('.results').append("<br>");

                if (notEmpty(description)) {
                    $('.results').append(`${description}<br>`);
                    $('.results').append("<br>");
                }

                if (notEmpty(style)) {
                    $('.results').append(`Style Description: ${style.description}<br>`);
                }

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
            $('.media-right').empty();

            let beer = response.data;
            let abv = beer.abv;
            let description = beer.description;
            let image = beer.labels;
            let style = beer.style;

            let brewery = beer.breweries[0];
            let breweryDesc = brewery.description;
            let breweryName = brewery.name;
            let website = brewery.website;

            if (notEmpty(image)) {
                $('.media-right').wrapInner(`<a href="${image.large}"><img class="media-object" src="${image.medium}" height="128px"></a>`);
            } else {
                $('.media-right').wrapInner(`<img class="media-object" src="/images/noun_3235_cc-gry2-lg.svg" height="128px">`);
            }
            
            $('.top').append(`<h4>${beer.name}</h4>`);

            if (notEmpty(style)) {
                $('.top').append(`<h4>${style.name}</h4>`);
            }
            if (notEmpty(breweryName)) {
                $('.top').append(`<h4>${breweryName}</h4>`);
            }
            if (notEmpty(abv)) {
                $('.top').append(`<h4>ABV: ${abv}%</h4>`);
            }

            $('.info').append(`<br>`);

            if (notEmpty(description)) {
                $('.info').append(`<p><b>Beer Description</b></p>`);
                $('.info').append(`<p>${description}</p><br>`);
            }
            if (notEmpty(style)) {
                $('.info').append(`<p><b>Style Description</b></p>`);
                $('.info').append(`<p>${style.description}</p><br>`);
            }
            if (notEmpty(breweryDesc)) {
                $('.info').append(`<p><b>Brewery Description</b></p>`);
                $('.info').append(`<p>${breweryDesc}</p>`);
            }
            if (notEmpty(website)) {
                $('.info').append(`<p><a href="${website}">Brewery Website</a></p>`);
            }

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
    };
    if ($('.results').length) {
        getBeers($.urlParam('beer'), $.urlParam('pageNum'));
    }
    if ($('.info').length) {
        singleBeer($.urlParam('id'));
    }
});
