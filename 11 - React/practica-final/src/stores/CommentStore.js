import Reflux from "reflux";
import io from "socket.io-client"
import CommentActions from "../actions/CommentActions";

let CommentStore = Reflux.createStore({
    listenables: [CommentActions],
    init: function() {
        this.socket = io("http://localhost:8080/");
        this.socket.on("data", (data) => {
            this.trigger(data);
        })
    },
    fetchComments: function() {
        this.socket.emit("read");
    },
    sendSign: function(comment) {
        this.socket.emit("sign", comment);
    }
})

export default CommentStore;