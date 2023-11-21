// https://es.wikipedia.org/wiki/Webpack
// https://nodejs.org/docs/latest/api/modules.html#__dirname

// Importa el modulo webpack y el modulo path de Node.js
const webpack = require("webpack");
const path = require("path");

// isDebug se estable como verdadero si el entorno Node.js no esta configurado como produccion.
const isDebug = process.env.NODE_ENV !== "production";

// Exporta un objeto que contiene la configuracion del webpack.
module.exports = {
    // Directorio base para resolver las rutas de entrada y salida.
    // __dirname es una variable global de Node.js que representa el directorio actual.
    context: __dirname,
    // Punto de entrada de la aplicacion, archivo principal desde el cual se instalara el paquete.
    entry: "./app/dist/index.js",
    // Salida de los archivos generados por el webpack.
    output: {
        // Directorio de salida de los archivos generados, se construye la ruta con la variable
        // global __dirname en la carpeta app/js.
        path: path.resolve(__dirname, "app/js"),
        // Es el nombre que tendra el archivo de salida.
        filename: "index.min.js",
    },
    // Se encarga de definir el tipo de fuente de mapa que se generará durante la compilación. 
    // Los mapas de origen son archivos adicionales que mapean el código fuente original a los 
    // archivos generados por Webpack, lo que facilita la depuración al mostrar líneas de código
    // originales en lugar del código generado.
    devtool: isDebug ? "inline-source-map" : false,
    // Se establece el modo de construccion del webpack. 
    // En modo de desarrollo, se optimiza para la velocidad y facilita la depuración. 
    // En modo de producción, se optimiza para el tamaño del archivo de salida.
    mode: isDebug ? "development" : "production",
    // Configuracion de los complementos del webpack.
    // En modo de desarrollo no se utiliza ningun complemento.
    // En modo de producción, se utilizan algunos complementos para optimizar el código.
    plugins: isDebug
        ? []
        : [
            // Complemento que ayuda a minimizar el tamaño del archivo resultante.
            new webpack.optimize.OccurrenceOrderPlugin(),
            // Complemento que minimiza y ofusca el código JavaScript. 
            new webpack.optimize.UglifyJsPlugin({
                // Se configura para no ofuscar.
                mangle: false,
                // Desactiva la generación de mapas de origen.
                sourcemap: false,
            }),
        ],
};
