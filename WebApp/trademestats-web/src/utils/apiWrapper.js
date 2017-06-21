var axios = require('axios');
var apiUrl = process.env.NODE_ENV === 'production' ? process.env.REACT_APP_PROD_API_URL
	: process.env.REACT_APP_TEST_API_URL;

function GetRentalListings(){
	return axios.get(apiUrl + '/rentallistings')
		.then(function(listings){
			return listings.data;
		});
}

function GetLocalities(){
	return axios.get(apiUrl + '/localities')
		.then(function(localities){
			return localities.data;
		});
}

function GetRentalListingsForLocality(localityId){
	return axios.get(apiUrl + '/rentallistings/locality/' + localityId)
		.then(function(localities){
			return localities.data;
		});
}

function GetForSaleListings(){
	console.log('get for sale');
}

module.exports = {
	getRentalListings: GetRentalListings,
	getForSaleListings: GetForSaleListings,
	getLocalities: GetLocalities,
	getRentalListingsForLocality: GetRentalListingsForLocality
}