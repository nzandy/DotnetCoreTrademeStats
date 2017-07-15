var React = require('react');
var PropTypes = require('prop-types');

class DropdownMenu extends React.Component {
	constructor(props){
		super(props);

		this.state = {
			items: [],
			value: ''
		};

		this.handleChange = this.handleChange.bind(this);
	}

	componentDidMount(){
		
		this.props.loadItems()
			.then(function(items){
				this.setState(function(){
					return {
						items: items,
						value: this.props.defaultValue // Make sure 'all' is selected by default.
					}
				})
		}.bind(this));
	}

	handleChange(event) {
		var selectedId = event.target.value;
		this.setState({value: selectedId});
		this.props.onChange(selectedId);
	}

	render(){
		return (
			<div className='dropdown-menu'>
				<label htmlFor='dropdown-select'> {this.props.labelText} </label>
				<select name='dropdown-select' value={this.state.value} onChange={this.handleChange}>
					{this.state.items.map(function(item, index){
						return (
							<option
								value={this.props.getItemId(item)} 
								key={this.props.getItemId(item)}>
								{this.props.getItemName(item)} 
							</option>
						)
					}.bind(this))}
				</select>
			</div>
		)
	}

}

DropdownMenu.propTypes = {
	loadItems: PropTypes.func.isRequired,
	onChange: PropTypes.func.isRequired,
	getItemId: PropTypes.func.isRequired,
	getItemName: PropTypes.func.isRequired,
	labelText: PropTypes.string.isRequired,
	defaultValue: PropTypes.number.isRequired
}

module.exports = DropdownMenu;