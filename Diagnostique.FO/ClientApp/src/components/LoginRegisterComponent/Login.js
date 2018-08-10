import React, { Component } from "react";
import { Route } from 'react-router';
import { Button, FormGroup, FormControl, ControlLabel } from "react-bootstrap";
import "./Login.css";
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'react-router-redux';
import { createBrowserHistory } from 'history';
import { BrowserRouter, Link, Redirect, withRouter } from 'react-router-dom';
import configureStore from './../../store/configureStore';
import App from "../../App";

const rootElement = document.getElementById('root');
// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const history = createBrowserHistory({ basename: baseUrl });

// Get the application-wide store instance, prepopulating with state from the server where available.
const initialState = window.initialReduxState;
const store = configureStore(history, initialState);


export default class Login extends Component {
    constructor(props) {
        super(props);

        this.state = {
            email: "",
            password: "",
            userInfos: [], 
            isAuthenticated : false
        };
    }

    validateForm() {
        return this.state.email.length > 0 && this.state.password.length > 0;
    }

    handleChange = event => {
        this.setState({
            [event.target.id]: event.target.value
        });
    }

    handleSubmit = event => {
        event.preventDefault();
              
        this.setState({ userInfos: [], isAuthenticated: false });  

        fetch(`api/LoginFo/GetUserInfosAsync?mail=${this.state.email}&mdp=${this.state.password}`)
            .then(response => response.json())
            .then(data => {
                console.log(this.state.userInfos);
                this.setState({ userInfos: data, isAuthenticated: true });     
                ReactDOM.render(
                    <Provider store={store}>
                        <BrowserRouter history={history}>
                            <App />
                        </BrowserRouter>
                    </Provider>,
                    rootElement);          
            }).catch((error) => {
                this.setState({ userInfos: [], isAuthenticated: false }); 
            });
        //console.log(this.state.userInfos);
        //console.log(this.state.isAuthenticated);
    }

    render() {
        return (
            <div className="Login">
                <form onSubmit={this.handleSubmit}>
                    <FormGroup controlId="email" bsSize="large">
                        <ControlLabel>Email</ControlLabel>
                        <FormControl
                            autoFocus
                            type="email"
                            value={this.state.email}
                            onChange={this.handleChange}
                        />
                    </FormGroup>
                    <FormGroup controlId="password" bsSize="large">
                        <ControlLabel>Password</ControlLabel>
                        <FormControl
                            value={this.state.password}
                            onChange={this.handleChange}
                            type="password"
                        />
                    </FormGroup>
                    <Button
                        block
                        bsSize="large"
                        disabled={!this.validateForm()}
                        type="submit"
                    >
                        Login
          </Button>
                </form>
            </div>
        );
    }
}
