import React from "react";
import { Link } from "react-router-dom";

export default class Home extends React.Component {
    constructor() {
        super();
    }

    render() {
        return (
            <div>
                <h1>Hola</h1>
                <Link to="store">Store</Link>
            </div>
        );
    }
}