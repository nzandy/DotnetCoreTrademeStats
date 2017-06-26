var React = require('react');
var PropTypes = require('prop-types');
var DropdownMenu = require('./DropdownMenu');
var Api = require('../utils/apiWrapper');

class LocalityDropdownMenu extends React.Component {
	loadRentalListings(){
		return Api.getLocalities();
	}

	getRentalListingId(listing){
		return listing.LocalityId;
	}

	getRentalListingName(listing){
		return listing.name;
	}

	render(){
		return ( <DropdownMenu 
			loadItems={this.loadRentalListings} 
			onChange={this.props.onChange} 
			labelText='Filter by Region: '
			defaultValue={100} 
			getItemId={this.getRentalListingId}
			getItemName={this.getRentalListingName}
		/> )
	}
}

LocalityDropdownMenu.propTypes = {
	onChange: PropTypes.func.isRequired
}

module.exports = LocalityDropdownMenu;