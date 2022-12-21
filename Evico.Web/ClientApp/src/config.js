const config = {

    api: process.env.REACT_APP_API,
    host: process.env.REACT_APP_HOST,
    authBearerToken: 'authBearerToken',
    authRefreshToken: 'authRefreshToken',
    redirect_uri_auth: process.env.REACT_APP_REDIRECT_URI_AUTH,
    bearerToken: localStorage.getItem('authBearerToken')
};

export default config;