import React, { Component } from 'react';
import logo from '../logo.svg';
import '../App.css';

var Api = require('../utils/apiWrapper');

class App extends Component {
	render() {
		return (
			<div className="App">
				<div className="App-header">
					<img src={logo} className="App-logo" alt="logo" />
					<h2>House Rental / Sales NZ.</h2>
					{console.log(process.env.REACT_APP_API_URL)}
					{Api.getRentalListings()}
					{Api.getForSaleListings()}
				</div>
				<p className="App-intro">
					To get started, edit <code>src/App.js</code> and save to reload.
				</p>
			</div>
		);
	}
}

export default App;
