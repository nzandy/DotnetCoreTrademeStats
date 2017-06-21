var React = require('react');
var Api = require('../utils/apiWrapper');

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
		console.log(event.target.value);
		this.setState({value: event.target.value});
	}

	render(){
		return (
			<div>
				<p> Filter by Region: </p>
				<select value={this.state.value} onChange={this.handleChange}>
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

module.exports = DropdownMenu;