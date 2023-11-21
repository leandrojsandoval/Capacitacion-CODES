const path = require("path");
const basePath = __dirname
const distPath = "dist"

// HTML
const HtmlWebpackPlugin = require("html-webpack-plugin");

const TerserPlugin = require('terser-webpack-plugin');

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
    },
    optimization: {
        minimizer: [
            new TerserPlugin({
                terserOptions: {
                    ecma: undefined,
                    warnings: false,
                    parse: {},
                    compress: {},
                    mangle: true,
                    module: false,
                    output: null,
                    toplevel: false,
                    nameCache: null,
                    ie8: false,
                    keep_fnames: false,
                },
            }),
        ],
    }
}