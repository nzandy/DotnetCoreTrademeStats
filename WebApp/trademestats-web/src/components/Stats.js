var React = require('react');
var LocalityDropdownMenu = require('./LocalityDropdownMenu');
var Api = require('../utils/apiWrapper');
var PropTypes = require('prop-types');

class Stats extends React.Component{

	constructor(props){
		super(props);
		this.state = {
			localityStatistics: false
		}

		this.handleDropdownChange = this.handleDropdownChange.bind(this);
	}

	componentDidMount(){
		this.handleDropdownChange(100);
	}

	handleDropdownChange(localityId){
		Api.getRentalListingStatsForLocality(localityId)
			.then(function(localityStatistics){
				this.setState(function(){
					return {
						localityStatistics: localityStatistics
					}
				});
			}.bind(this));
	}

	render(){
		return(
			<div className='stats-container'>  
				<LocalityDropdownMenu onChange={this.handleDropdownChange}/>
			
				{!this.state.localityStatistics
					? <p>Loading</p>
					: <LocalityStatisticDisplay statistic={this.state.localityStatistics}/>
				}
			</div>
		)
	}
}

class LocalityStatisticDisplay extends React.Component {
	render(){
		return (
			<div className='locality-stats'>
				<p> Number of listings: {this.props.statistic.listingCount} </p>
				<p> Average rent per listing: ${this.props.statistic.averageRentPerWeek.toFixed(2)}</p>
				<p> Average rent per room: ${this.props.statistic.averageRentPerRoom.toFixed(2)} </p>
				<p> Highest rent per week: ${this.props.statistic.highestRentPerWeek.toFixed(2)} </p>
				<p> Lowest rent per week: ${this.props.statistic.lowestRentPerWeek.toFixed(2)} </p>
			</div>
		)
	}
}
LocalityStatisticDisplay.propTypes = {
	statistic: PropTypes.object.isRequired
}

module.exports = Stats;