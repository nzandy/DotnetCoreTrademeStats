var React = require('react');
var Home = require('./Home');
require('../App.css');
var RentalListings = require('./RentalListings');
var ReactRouter = require('react-router-dom');
var Router = ReactRouter.BrowserRouter;
var Route = ReactRouter.Route;
var Switch = ReactRouter.Switch;


class App extends React.Component {
	render() {
		return (
			<Router>
			<div className="container">
				<div className="App-header">
					<h2>House Rentals NZ.</h2>
				</div>
				<Switch>
					<Route exact path='/' component={Home} />
					<Route exact path='/rentals' component={RentalListings} />
					<Route render={function (){
							return (
								<p> Not Found </p>
							)
						}} />
				</Switch>
			</div>
			</Router>
		)
	}
}

module.exports = App;
