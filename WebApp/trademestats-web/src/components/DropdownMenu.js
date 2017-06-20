var React = require('react');
var Api = require('../utils/apiWrapper');

class DropdownMenu extends React.Component {
	constructor(props){
		super(props);

		this.state = {
			localities: []
		};
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

	render(){
		return (
			<div>
				<p> Filter by Region: </p>
				<select>
					{this.state.localities.map(function(locality, index){
						return (
							<option value={locality.name} key={locality.LocalityId}>{locality.name} </option>
						)
					})}
				</select>
			</div>
		)
	}

}

module.exports = DropdownMenu;