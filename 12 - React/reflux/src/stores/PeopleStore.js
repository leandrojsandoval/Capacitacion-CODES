import Reflux from "reflux";
import PeopleActions from "../actions/PeopleActions";
import io from "socket.io-client"

let PeopleStore = Reflux.createStore({
    listenables: [PeopleActions],
    fetchPeople: function () {
        this.socket = io("http://localhost:8080");
        this.socket.on("people", (people) => {
            var people = JSON.parse(people);
            people = people.results[0];
            this.trigger(people);
        });
    },
    askForPeople: function () {
        this.socket.emit("ask");
    }
});

export default PeopleStore;
