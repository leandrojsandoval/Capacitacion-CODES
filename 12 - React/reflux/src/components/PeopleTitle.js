import React from "react";

export default class PeopleTitle extends React.Component {

    constructor() {
        super();
    }

    render() {
        const name = this.props.title;
        return (<div><h1>{name ? `${name.title} ${name.first} ${name.last}` : "N/A"}</h1></div>);
    }
}
