import config from '../../../config';

export default function AuthVkCallback() {

    const urlReg = /https:\/\/oauth.vk.com\/\//;
    const urlParams = new URLSearchParams(window.location.search);
    const host = config.host;
    const redirectUrl = config.redirect_uri_auth;

    try {
        
        if (!urlReg.test(document.referrer)) throw new Error('Неверный url');
        
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
                if (response.status === 200 || response.status === 202) {
                    let responseObj = JSON.parse(responseStr);
                    //todo: строковые константы лучше бы вынести (authBearerToken и authRefreshToken)
                    localStorage.setItem(config.authBearerToken, responseObj['bearerToken']);
                    localStorage.setItem(config.authRefreshToken, responseObj['refreshToken']);
                    window.close();
                    return;
                }

                throw new Error(responseStr)
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