import React from "react";

import CommentForm from "./CommentForm.js";
import ComentList from "./CommentList.js";

export default class CommentBox extends React.Component {

    constructor() {
        super();
    }

    render() {
        return (
            <div className="commentBox">
                <CommentForm onSubmit={this.props.onSubmit}/>
                <ComentList data={this.props.data} />
            </div>
        )
    }
}