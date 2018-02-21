"use strict";

// Use to check if data exists in object
const notEmpty = value => typeof value !== "undefined";

// Get parameters from url
$.urlParam = function (name) {
    var results = new RegExp("[?&]" + name + "=([^&#]*)").exec(
        window.location.href
    );
    if (results === null) {
        return null;
    } else {
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
                $(".media-right").html(
                    `<a href="${image.large}"><img src="${image.medium}"></a>`
                );
            } else {
                $(".media-right").html(`<img src="/images/default_logo_gray.svg">`);
            }

            $("img").addClass("media-object");
            $(".media-object").css("height", "128px");

            $(".media-body").append(`<h4>${beer.name}</h4>`);

            if (notEmpty(style)) {
                $(".media-body").append(`<h4>${style.name}</h4>`);
            }
            if (notEmpty(breweryName)) {
                $(".media-body").append(`<h4>${breweryName}</h4>`);
            }
            if (notEmpty(abv)) {
                $(".media-body").append(`<h4>ABV: ${abv}%</h4>`);
            }

            $(".info").append(`<br>`);

            if (notEmpty(description)) {
                $(".info")
                    .append(`<p><b>Beer Description</b></p>`)
                    .append(`<p>${description}</p><br>`);
            }
            if (notEmpty(style)) {
                $(".info")
                    .append(`<p><b>Style Description</b></p>`)
                    .append(`<p>${style.description}</p><br>`);
            }
            if (notEmpty(breweryDesc)) {
                $(".info")
                    .append(`<p><b>Brewery Description</b></p>`)
                    .append(`<p>${breweryDesc}</p>`);
            }
            if (notEmpty(website)) {
                $(".info").append(`<p><a href="${website}">Brewery Website</a></p>`);
                $(".info a").attr("target", "_blank");
            }

            $(".info")
                .append(`<br>`)
                .append(
                `<a href="/Entry/Add?id=${beer.id}&name=${beer.name}" role="button">Add ${beer.name} To Your Log</a>`
                );

            $("a[role='button']").addClass("btn btn-success");
        }
    });
}

$(document).ready(function () {
    if ($(".info").length) {
        singleBeer($.urlParam("id"));
    }
    $(".logo-box").hide().fadeIn(1600);
});

