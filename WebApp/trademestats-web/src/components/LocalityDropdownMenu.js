var React = require('react');
var PropTypes = require('prop-types');
var DropdownMenu = require('./DropdownMenu');
var Api = require('../utils/apiWrapper');

class LocalityDropdownMenu extends React.Component {
	loadLocalities(){
		return Api.getLocalities();
	}

	getLocalityId(locality){
		return locality.LocalityId;
	}

	getLocalityName(locality){
		return locality.name;
	}

	render(){
		return ( <DropdownMenu 
			loadItems={this.loadLocalities} 
			onChange={this.props.onChange} 
			labelText='Filter by Region: '
			defaultValue={100} 
			getItemId={this.getLocalityId}
			getItemName={this.getLocalityName}
		/> )
	}
}

LocalityDropdownMenu.propTypes = {
	onChange: PropTypes.func.isRequired
}

module.exports = LocalityDropdownMenu;