const webpack = require("webpack");
const path = require("path");

const isDebug = process.env.NODE_ENV !== "production";

module.exports = {
    context: __dirname,
    entry: "./app/dist/index.js",
    module: {
        rules: [
            {
                test: /\.jsx?$/,
                exclude: /(node_modules|bower_components)/,
                use: {
                    loader: "babel-loader",
                    options: {
                        presets: ["@babel/preset-react", "@babel/preset-env"],
                        plugins: ["babel-plugin-react-html-attrs", "transform-class-properties", "transform-decorators-legacy"],
                    }                                  
                }
            }
        ]
    },
    output: {
        path: path.resolve(__dirname, "app/js"),
        filename: "index.min.js",
    },
    devtool: isDebug ? "inline-source-map" : false,
    mode: isDebug ? "development" : "production",
    plugins: isDebug
        ? []
        : [
            new webpack.optimize.OccurrenceOrderPlugin(),
            new webpack.optimize.UglifyJsPlugin({
                mangle: false,
                sourcemap: false,
            }),
        ],
};