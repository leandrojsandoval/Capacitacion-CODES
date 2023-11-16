import React from "react";

export default class PeoplePhoto extends React.Component {

    constructor() {
        super();
    }

    render() {
        const { photo } = this.props;

        return (
            <div>
                {photo ?
                    <img src={photo.large} alt="people img" /> :
                    "No photo available"}
            </div>
        );
    }

}
