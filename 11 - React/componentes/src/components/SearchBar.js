import React from "react";

export default class SearchBar extends React.Component {

    constructor() {
        super()
    }

    render() {
        return (
            <div>
                <input onChange={this.props.onChange} type="text"></input>
                <input type="checkbox"></input>
            </div>
        )
    }

}