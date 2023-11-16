"use strict";
require("core-js/fn/object/assign");
const webpack = require("webpack");
const WebpackDevServer = require("webpack-dev-server");
const config = require("./webpack.config");
const open = require("open");

const express = require("express");
const http = require("http");
const engine = require("socket.io");
const cors = require("cors");
const request = require("request");

const port = 8080;
const app = express();

app.use(cors());

let server = http.createServer(app).listen(port, () => {
    console.log("Port listening in " + port);
});

const io = engine.listen(server);

io.on("connection", (socket) => {

    request("https://randomuser.me/api/", (err, response, body) => {
        if (!err && response.statusCode === 200) {
            socket.emit("people", body);
        } else {
            console.error("Error fetching random user:", err);
        }
    });

    socket.on("ask", () => {
        request("https://randomuser.me/api/", (err, response, body) => {
            if (!err && response.statusCode === 200) {
                socket.emit("people", body);
            } else {
                console.error("Error fetching random user:", err);
            }
        });
    });

});

new WebpackDevServer(webpack(config), config.devServer).listen(config.port, "localhost", (err) => {
    if (err) {
        console.log(err);
    }
    console.log("Listening at localhost:" + config.port);
    console.log("Opening your system browser...");
    open("http://localhost:" + config.port + "/webpack-dev-server/");
});
