import React from "react";

export default class CommentForm extends React.Component {

    constructor() {
        super();
    }

    render() {
        return (
            <form onSubmit={this.props.onSubmit} className="commentFrom">
                <input type="text" name="author" placeholder="Nombre" />
                <input type="text" name="author" placeholder="Firma" />
                <input type="hidden" name="id" value={Date.now()} />
                <input type="submit" value="Enviar" />
            </form>
        )
    }
}