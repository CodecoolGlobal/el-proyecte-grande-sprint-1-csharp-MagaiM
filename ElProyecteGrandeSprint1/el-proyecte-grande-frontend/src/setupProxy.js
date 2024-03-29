﻿const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    "/Home",
    "/Deals",
    "/News",
    "/User"

];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://el-proyecte-grande-kvm-gaming.herokuapp.com/',
        secure: false,
        changeOrigin: true,
    });

    app.use(appProxy);
};