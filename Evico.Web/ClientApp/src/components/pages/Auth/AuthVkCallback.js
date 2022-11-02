import Button from "../../elements/Buttons/Button";
import Icon from "../../elements/Icons/Icon";

export default function AuthVkCallback() {
    //todo: тут не помешала бы проверка на referer url. должны обрабатывать запросы только с referer url = https://oauth.vk.com/
    
    const urlParams = new URLSearchParams(window.location.search);
    //const host = 'https://api.csu-evico.ru:61666/' //todo: вынести в конфиг
    const host = 'https://localhost:61666/'
    
    
    try {
        fetch(`${host}auth/vkGateway`, {
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
                    localStorage.setItem('authBearerToken', responseObj['bearerToken']);
                    localStorage.setItem('authRefreshToken', responseObj['refreshToken']);
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