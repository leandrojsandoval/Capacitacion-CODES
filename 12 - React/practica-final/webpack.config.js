const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");

const basePath = __dirname;
const distPath = "dist";

module.exports = {
  mode: "production",
  entry: {
    app: "./src/index.js",
  },
  module: {
    rules: [
      {
        test: /\.js$/,
        exclude: /node_modules/,
        use: ["babel-loader"],
      },
      {
        test: /\.css$/,
        use: ["style-loader", "css-loader"],
      },
    ],
  },
  plugins: [
    new HtmlWebpackPlugin({
      template: "src/index.html",
      minify: false,
      scriptLoading: "blocking",
    }),
  ],
  output: {
    path: path.join(basePath, distPath),
    filename: "index.min.js",
  },
};
