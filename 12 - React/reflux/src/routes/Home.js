import React from "react";
import ReactMixin from "react-mixin";
import Reflux from "reflux";

import PeopleActions from "../actions/PeopleActions";
import PeopleFrame from "../components/PeopleFrame";
import PeopleStore from "../stores/PeopleStore";

@ReactMixin.decorate(Reflux.connect(PeopleStore, "people"))
export default class Home extends React.Component {

    constructor() {
        super();
    }

    componentDidMount() {
        PeopleActions.fetchPeople();
    }
    
    handlePeople() {
        PeopleActions.askForPeople();
    }  

    render() {
        if (this.state && this.state.people != null) {
            return (
                <div>
                    <PeopleFrame person={this.state.people.people} />
                    <button onClick={this.handlePeople}>ASK</button>
                </div>
            );
        } else {
            return (<h1>Loading</h1>);
        }
    }
}
