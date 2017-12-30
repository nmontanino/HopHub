﻿'use strict';

function getBeers(query, pageNum) {
    $.ajax({
        url: "/home/getbeer?beer=" + query + "&pageNum=" + pageNum,
        success: function (response) {
            console.log(response);

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

            for (let i = 0; i < beers.length; i++) {

                let name = beers[i].name;
                let style = beers[i].style;
                let abv = beers[i].abv;
                let description = beers[i].description;
                let beerId = beers[i].id;
                let brewery = beers[i].breweries[0];

                // Use placeholder image if label not available
                let image = beers[i].labels;

                if (notEmpty(image)) {
                    $('.results').append(`<div class="pull-right"><img src="${image.icon}"><div>`);
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
$(document).ready(function () {
    getBeers($.urlParam('beer'), $.urlParam('pageNum'));
});
