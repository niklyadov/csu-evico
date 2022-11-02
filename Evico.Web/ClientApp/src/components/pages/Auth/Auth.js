import Button from "../../elements/Buttons/Button";
import Icon from "../../elements/Icons/Icon";

export default function Auth() {

    const clientId = 51458458, redirect_uri = 'http://web.csu-evico.ru:60666/Auth/VkGateway';

    return <div

        id='auth'
        className='div-window'

    >

        <h1>Авторизация</h1>
        <Icon
            
            alt='Auth'
            src='https://play-lh.googleusercontent.com/GntsGclzheXXASOhjSF1lCOPOznM_OARDObiTW_NQZtpYVwPQr_0ARyRyiXB0_OocmI'
            onClick={_ => {

                window.open(

                    `https://oauth.vk.com/authorize?client_id=${clientId}&redirect_uri=${redirect_uri}&scope=12&display=mobile`,
                    'Auth',
                    `scrollbars=no,resizable=no,status=no,location=no,toolbar=no,menubar=no,width=800,height=500`

                );

                (() => {

                    return new Promise((resolve, reject) => {

                        setTimeout(() => reject('Auth error'), 60000);

                        let awaiterInterval = setInterval(() => {

                            const cookies = document.cookie.split(';').reduce((ac, str) => Object.assign(ac, { [str.split('=')[0].trim()]: str.split('=')[1] }), {});

                            if (cookies['bearerToken'] && cookies['refreshToken']) {

                                clearInterval(awaiterInterval);
                                return resolve();

                            }

                        }, 500);

                    });

                })().then(() => {

                    alert('Успешная авторизация. Смотри куки.')

                }).catch((reason) => {

                    alert(reason)

                });

            }}

        />

    </div>

};