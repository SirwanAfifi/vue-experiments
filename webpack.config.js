let webpack = require('webpack');
const VueLoaderPlugin = require('vue-loader/lib/plugin');
let path = require('path');

module.exports = {
    entry: {
        main: './ClientApp/main'
    },
    output: {
        path: path.resolve(__dirname, 'wwwroot', 'js'),
        filename: '[name].js',
        publicPath: '/js/'
    },
    module: {
        rules: [
            { test: /\.vue$/, loader: "vue-loader" },
            {
                test: /\.tsx?$/,
                loader: 'ts-loader',
                exclude: /node_modules/,
                options: { appendTsSuffixTo: [/\.vue$/] }
            }
        ]
    },
    resolve: {
        extensions: ['.tsx', '.ts', '.js'],
        alias: {
            'vue$': 'vue/dist/vue.esm.js' // 'vue/dist/vue.common.js' for webpack 1
        }
    },
    optimization: {
        splitChunks: {
            cacheGroups: {
                commons: {
                    test: /[\\/]node_modules[\\/]/,
                    name: "main",
                    chunks: "all"
                }
            }
        }
    },
    plugins: [
        new VueLoaderPlugin()
    ]
};