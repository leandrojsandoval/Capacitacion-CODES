import React from "react";
import { Link } from "react-router-dom";

export default class Home extends React.Component {

    constructor() {
        super();
    }

    render() {
        return (
            <div className="home">
                <h1>Agrega una firma</h1>
                <Link to="sign">Firmar ahora</Link>
            </div>
        )
    }
}