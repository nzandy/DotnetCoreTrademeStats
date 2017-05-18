var axios = require('axios');
var apiUrl = process.env.NODE_ENV === 'production' ? process.env.REACT_APP_PROD_API_URL
	: process.env.REACT_APP_TEST_API_URL;

function GetRentalListings(){
	console.log('get rentals');
}

function GetForSaleListings(){
	console.log('get for sale');
}

module.exports = {
	getRentalListings: GetRentalListings,
	getForSaleListings: GetForSaleListings
}