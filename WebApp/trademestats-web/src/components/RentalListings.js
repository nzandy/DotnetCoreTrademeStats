var React = require('react');
var Api = require('../utils/apiWrapper');
var PropTypes = require('prop-types');
var LocalityDropdownMenu = require('./LocalityDropdownMenu');


class RentalListings extends React.Component{
	constructor(props){
		super(props);

		this.state = {
			listings: null
		}

		this.handleDropdownChange = this.handleDropdownChange.bind(this);
	}

	componentDidMount(){
		Api.getRentalListings()
			.then(function(listings){
				this.setState(function(){
					return {
						listings: listings
					}
				})
		}.bind(this));
	}

	handleDropdownChange(localityId){
		Api.getRentalListingsForLocality(localityId)
			.then(function(filteredListings){
				this.setState(function(){
					return {
						listings: filteredListings
					}
				});
			}.bind(this));
	}

	render(){
		return(
			<div className='rental-listing-container'>
				<LocalityDropdownMenu 
					onChange={this.handleDropdownChange} 
					/>
					{!this.state.listings
						? <p>Loading</p>
						: <ListingGrid listings={this.state.listings}/>
					}
			</div>
		)
	}
}

class ListingGrid extends React.Component{
	render(){
		return(
			<div className='results'>
				<p> Found {this.props.listings.length} Listings. </p>
				<div className='listings-container'>
					{this.props.listings.map(function(listing, index){
						return (
							<div key={listing.ListingId} className='listing'>
								<p>{listing.title}</p>
								<p>Price: ${listing.rentPerWeek}pw </p>
							</div>
						)
					})}
				</div>
			</div>
		)	
	}
}

ListingGrid.propTypes = {
	listings: PropTypes.array.isRequired
}

module.exports = RentalListings;