var React = require('react');
var LocalityDropdownMenu = require('./LocalityDropdownMenu');
var Api = require('../utils/apiWrapper');

class Stats extends React.Component{
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
			<div className='stats-container'>  
				<LocalityDropdownMenu onChange={this.handleDropdownChange}/>
			</div>
		)
	}
}

module.exports = Stats;