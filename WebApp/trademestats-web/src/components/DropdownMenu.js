var React = require('react');
var Api = require('../utils/apiWrapper');
var PropTypes = require('prop-types');

class DropdownMenu extends React.Component {
	constructor(props){
		super(props);

		this.state = {
			localities: [],
			value: ''
		};

		this.handleChange = this.handleChange.bind(this);
	}

	componentDidMount(){
		Api.getLocalities()
			.then(function(localities){
				this.setState(function(){
					return {
						localities: localities
					}
				})
		}.bind(this));
	}

	handleChange(event) {
		var localityId = event.target.value;
		this.setState({value: localityId});
		Api.getRentalListingsForLocality(localityId)
			.then(function(filteredListings){
				this.props.onChange(filteredListings);
			}.bind(this));
	}

	render(){
		return (
			<div className='dropdown-menu'>
				<label htmlFor='region-dropdown'> Filter by Region: </label>
				<select name='region-dropdown' value={this.state.value} onChange={this.handleChange}>
					{this.state.localities.map(function(locality, index){
						return (
							<option value={locality.LocalityId} key={locality.LocalityId}>{locality.name} </option>
						)
					})}
				</select>
			</div>
		)
	}

}

DropdownMenu.propTypes = {
	onChange: PropTypes.func.isRequired
}

module.exports = DropdownMenu;