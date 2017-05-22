var React = require('react');
var Api = require('../utils/apiWrapper');
var PropTypes = require('prop-types');

class RentalListings extends React.Component{
	constructor(props){
		super(props);

		this.state = {
			listings: null
		}
	}

	componentDidMount(){
		Api.getRentalListings()
			.then(function(listings){
				console.log(JSON.stringify(listings));
				this.setState(function(){
					return {
						listings: listings
					}
				})
		}.bind(this));
	}

	render(){
		return(
			<div className='rental-listings'>
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
			<div className='listings-container'>
				{this.props.listings.map(function(listing, index){
					return (
						<div key={listing.listingId} className='listing'>
							<p>{listing.title}</p>
							<p>Price: ${listing.rentPerWeek}pw </p>
						</div>
					)
				})}
			</div>
		)	
	}
}

ListingGrid.propTypes = {
	listings: PropTypes.array.isRequired
}

module.exports = RentalListings;