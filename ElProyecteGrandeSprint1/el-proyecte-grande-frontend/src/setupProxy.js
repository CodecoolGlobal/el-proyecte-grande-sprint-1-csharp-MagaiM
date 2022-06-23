const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    "/Home",
    "/Deals",
    "/News",
    "/User"

];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://localhost:44321',
        secure: false,
        changeOrigin: true,
    });

    app.use(appProxy);
};