var React = require('react');
import '../App.css';
var RentalListings = require('./RentalListings');


class App extends React.Component {
	render() {
		return (
			<div className="container">
				<div className="App-header">
					<h2>House Rentals NZ.</h2>
				</div>
				<RentalListings/> 
			</div>
		)
	}
}

module.exports = App;
