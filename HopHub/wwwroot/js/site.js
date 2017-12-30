'use strict';

// Use to check if data exists in object
const notEmpty = value => typeof value !== 'undefined';

// Get parameters from url
$.urlParam = function (name) {
    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
    if (results === null) {
        return null;
    }
    else {
        return decodeURI(results[1]) || 0;
    }
};

function singleBeer(beerId) {
    $.ajax({
        url: "/beer/singlebeer?id=" + beerId,
        success: function (response) {
            console.log(response);

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
                $('.media-right').html(`<a href="${image.large}"><img class="media-object" src="${image.medium}" height="128px"></a>`);
            } else {
                $('.media-right').html(`<img class="media-object" src="/images/noun_3235_cc-gry2-lg.svg" height="128px">`);
            }
            
            $('.media-body').append(`<h4>${beer.name}</h4>`);

            if (notEmpty(style)) {
                $('.media-body').append(`<h4>${style.name}</h4>`);
            }
            if (notEmpty(breweryName)) {
                $('.media-body').append(`<h4>${breweryName}</h4>`);
            }
            if (notEmpty(abv)) {
                $('.media-body').append(`<h4>ABV: ${abv}%</h4>`);
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
    if ($('.info').length) {
        singleBeer($.urlParam('id'));
    }
});
