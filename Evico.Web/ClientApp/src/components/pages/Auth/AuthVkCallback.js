import config from '../../../config';

export default function AuthVkCallback() {

    const urlReg = /https:\/\/oauth.vk.com\/\//;
    const urlParams = new URL(config.host + window.location.hash.replace('#', '')).searchParams
    console.log(urlParams)
    const host = config.api;
    const redirectUrl = config.redirect_uri_auth;

    try {
        fetch(`${host}auth/vkGateway?redirectUrl=${redirectUrl}`, {
            method: 'POST',
            mode: 'cors',
            headers: {
                'accept': 'text/plain',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(urlParams.get('code'))
        })
            .then(async response => {
                let responseStr = await response.text();
                let responseObj = JSON.parse(responseStr);
                localStorage.setItem(config.authBearerToken, responseObj['bearerToken']);
                localStorage.setItem(config.authRefreshToken, responseObj['refreshToken']);
                window.close();
            });
    } catch (ex) {
        console.log(ex)
        alert('failed!, see console')
    }

    return <div

        id='auth'
        className='div-window'

    >

        <h1>Authorization in progress . . .</h1>

    </div>

};