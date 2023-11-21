require("./styles/styles.css");
import "core-js/fn/object/assign";
import React from "react";
import ReactDom from "react-dom";
import { Router, Route } from "react-router";
import { createBrowserHistory } from "history";

import Home from "./routes/Home";
import Sign from "./routes/Sign";

const app = document.getElementById("app");
const history = createBrowserHistory();

ReactDom.render(
    <Router history={history}>
        <Route path="/" component={Home} />
        <Route path="sign" component={Sign} />
    </Router>, app);