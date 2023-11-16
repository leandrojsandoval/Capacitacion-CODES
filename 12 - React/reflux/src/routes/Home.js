import React from "react";
import ReactMixin from "react-mixin";
import Reflux from "reflux";

import PeopleActions from "../actions/PeopleActions";
import PeopleFrame from "../components/PeopleFrame";
import PeopleStore from "../stores/PeopleStore";

@ReactMixin.decorate(Reflux.connect(PeopleStore, "people"))
export default class Home extends React.Component {

    componentDidMount() {
        PeopleActions.fetchPeople();
    }

    constructor() {
        super();
    }

    render() {
        return (
            this.state && this.state.people != null ?
                (<PeopleFrame person={this.state.people.people} />) : (<h1>Loading</h1>)
        );
    }
    
}
