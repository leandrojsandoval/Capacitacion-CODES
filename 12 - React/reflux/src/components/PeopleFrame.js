import React from "react";
import PeoplePhoto from "./PeoplePhoto";
import PeopleTitle from "./PeopleTitle";

export default class PeopleFrame extends React.Component {
    constructor() {
        super();
    }

    render() {
        const person = this.props.person;
        return (
            <div>
                <PeoplePhoto photo={person.picture} />
                <PeopleTitle title={person.name} />
            </div>
        )
    }
}
