import React from "react";

export default class ProductCategoryRow extends React.Component {

    constructor() {
        super()
    }

    render() {
        return (
            <h4>{this.props.category}</h4>
        )
    }

}