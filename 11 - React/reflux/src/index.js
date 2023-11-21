import React from "react";
import ReactDOM from "react-dom";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";

import Home from "./routes/Home.js";

function App() {
    return (
        <Router>
            <Switch>
                <Route path="/" component={Home} />
            </Switch>
        </Router>
    );
}

const root = document.getElementById("app");
ReactDOM.render(<App />, root);
