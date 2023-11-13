// npm install --save-dev @babel/preset-react
// npm install react react-dom
// npm install react@latest react-dom@latest

const path = require("path");
const basePath = __dirname
const distPath = "dist"

// HTML
const HtmlWebpackPlugin = require("html-webpack-plugin");

module.exports = {
    // mode - modo de funcionamiento
    mode: "production",
    // entry point
    entry: {
        app: "./src/index.js"
    },
    module: {
        rules: [
            {
                test: /\.js/,
                exclude: /node_modules/,
                use: ["babel-loader"],
            }
        ]
    },
    plugins: [ 
        new HtmlWebpackPlugin({
            template: "src/index.html",
            minify: false,
            scriptLoading: "blocking",
        }),
    ],
    //output point
    output: {
        path: path.join(basePath, distPath),
        filename: "index.min.js"
    }
}