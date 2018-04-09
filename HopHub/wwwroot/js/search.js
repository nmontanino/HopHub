'use strict';

function searchBeers(query, pageNum) {
    $.ajax({
        url: "/home/searchbeers?beer=" + query + "&pageNum=" + pageNum,
        success: function (response) {
            console.log(response);

            let beers = response.data;
            let numResults = response.totalResults;
            let pages = response.numberOfPages;
            let currentPage = response.currentPage;

            // Create pager component if returning more than 50 results
            if (pages > 1) {
                if (currentPage > 1) {
                    $('.pager').append(`<li class="previous"><a href="?beer=${query}&pageNum=${currentPage - 1}">&larr; Previous</a></li>`);
                }
                if (currentPage !== pages) {
                    $('.pager').append(`<li class="next"><a href="?beer=${query}&pageNum=${currentPage + 1}">Next &rarr;</a></li>`);
                }
            }

            // Display number of results and page number
            if (beers) {
                $('.results')
                    .append(`<p><i>Displaying ${beers.length} out of ${numResults} results. (Page ${currentPage} of ${pages})</i></p>`)
                    .append("<br>");
            } else {
                $('.results').append("<i>No search results.</i>");
            }

            let resultsOnPage = beers.length;
            for (let i = 0; i < resultsOnPage; i++) {

                let name = beers[i].name;
                let style = beers[i].style;
                let abv = beers[i].abv;
                let description = beers[i].description;
                let beerId = beers[i].id;
                let brewery = beers[i].breweries;
                let image = beers[i].labels;


                //Checking for undefined values using ternary operators inside template strings. 
                /*
                let markup = `
                    <div class="well">
                        ${image ? `<div class="pull-right"><img src="${image.icon}"></div>` : `<div class="pull-right"><img class="thumbnail" src="/images/noun_3235_cc-gry2-lg.svg" height="64px"></div>`}
                        <b><a href="/Beer?id=${beerId}">${name}</a></b><br>
                        ${style ? `<b>${style.name}</b><br>` : ''}
                        ${brewery ? `<b>${brewery.name}</b><br>` : ''}
                        ${abv ? `<b>ABV: ${abv}%</b><br>` : ''}
                        <br>
                        ${description || ''}
                    </div>
                `;
                $('.results').append(markup);
                */

                // Use placeholder image if label not available
                if (image) {
                    $('.results').append(`<img src="${image.icon}">`);
                } else {
                    $('.results').append(`<img src="/images/default_logo_gray.svg">`);
                }

                $('img')
                    .addClass('pull-right')
                    .height(64);

                // Link to individual beer page
                $('.results').append(`<b><a href="/Beer?id=${beerId}">${name}</a></b><br>`);

                if (style) {
                    $('.results').append(`<b>${style.name}</b><br>`);
                }
                if (abv) {
                    $('.results').append(`<b>ABV: ${abv}%</b><br>`);
                }
                if (brewery) {
                    //$('.results').append(`<b><a href="/Home/Brewery?id=${brewery[0].id}">${brewery[0].name}</a></b><br>`);
                    $('.results').append(`<b>${brewery[0].name}</b><br>`)
                }

                $('.results').append("<br>");

                if (description) {
                    $('.results')
                        .append(description)
                        .append("<br>");
                }
                //if (notEmpty(style)) {
                //    $('.results').append(`Style Description: ${style.description}<br>`);
                //}

                $('.results').append("<hr>");
            }
        }
        
    });
}

$(document).ready(function () {
    searchBeers($.urlParam('beer'), $.urlParam('pageNum'));
});


