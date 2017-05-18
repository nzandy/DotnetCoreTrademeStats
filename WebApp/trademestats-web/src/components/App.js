import React, { Component } from 'react';
import logo from '../logo.svg';
import '../App.css';

var Api = require('../utils/apiWrapper');

class App extends Component {
	constructor(props){
		super(props);
		this.state = {
			listings: null
		}
	}

	componentDidMount(){
		Api.getRentalListings()
			.then(function(listings){
				console.log(JSON.stringify(listings));
				this.setState(function(){
					return {
						listings: listings
					}
				})
			}.bind(this))
	}

	render() {
		return (
			<div className="App">
				<div className="App-header">
					<img src={logo} className="App-logo" alt="logo" />
					<h2>House Rental / Sales NZ.</h2>
					<p>{this.state.listings ? JSON.stringify(this.state.listings)
						: 'Loading'}</p>
				</div>
				<p className="App-intro">
					To get started, edit <code>src/App.js</code> and save to reload.
				</p>
			</div>
		);
	}
}

export default App;
