import React from "react";

import Comment from "./Comment.js"

export default class CommentList extends React.Component {

    constructor() {
        super();
    }

    render() {
        let allComments = this.props.data.map((comment) => {
            return (<Comment key={comment.id} author={comment.author} text={comment.text}></Comment>)
        })
        return (
            <div class="commentList">
                {allComments}
            </div>
        )
    }
}