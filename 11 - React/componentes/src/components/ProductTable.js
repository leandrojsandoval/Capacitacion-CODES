import React from "react";
import ProductCategoryRow from "./ProductCategoryRow.js";
import ProductRow from "./ProductRow.js";

export default class ProductTable extends React.Component {

    constructor() {
        super()
    }

    render() {
        let rows = [];
        let last_category = null;
        if (this.props.filter != null && this.props.products != null) {
            let filter = this.props.filter;
            this.props.products.forEach((product) => {
                if (product.name.indexOf(filter) > -1) {
                    if (product.category != last_category)
                        rows.push(<ProductCategoryRow category={product.category} key={product.category} />);
                    rows.push(<ProductRow product={product} key={product.name} />);
                    last_category = product.category;
                }
            });
        } else if (this.props.products != null) {
            this.props.products.forEach((product) => {
                if (product.category != last_category)
                    rows.push(<ProductCategoryRow category={product.category} key={product.category} />);
                rows.push(<ProductRow product={product} key={product.name} />);
                last_category = product.category;
            })
        }
        else {
            rows.push(<h1>Loading</h1>);
        }

        return (<div>{rows}</div>)
    }

}