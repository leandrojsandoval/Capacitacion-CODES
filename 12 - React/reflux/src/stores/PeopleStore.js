import Reflux from "reflux";
import PeopleActions from "../actions/PeopleActions";
import $ from "jquery";

let PeopleStore = Reflux.createStore({
    listenables: [PeopleActions],
    init: function () {
        this.state = {
            people: null
        };
    },
    fetchPeople: function () {
        $.ajax({
            url: "https://randomuser.me/api/",
            dataType: "JSON"
        })
            .done(function (data) {
                let person = data.results[0];
                this.state.people = person;
                this.trigger(this.state);
            }.bind(this))
            .fail(function () {
                console.log("Error");
            });
    }
});

export default PeopleStore;