import config from "../../../config";
import Icon from "../../elements/Icons/Icon";

export default function Auth() {

    const clientId = 51458458, redirect_uri_auth = config.redirect_uri_auth;

    return <div

        id='auth'
        className='div-window'

    >

        <h1>Авторизация</h1>
        <Icon
            
            alt='Auth'
            src='https://play-lh.googleusercontent.com/GntsGclzheXXASOhjSF1lCOPOznM_OARDObiTW_NQZtpYVwPQr_0ARyRyiXB0_OocmI'
            onClick={_ => {

                // todo: перенести куда-то, в отдельный сервис?
                // function logout() {
                //     //todo: строковые константы лучше бы вынести (authBearerToken и authRefreshToken)
                //     localStorage.setItem('authBearerToken', null);
                //     localStorage.setItem('authRefreshToken', null);
                // }
                
                window.open(

                    `https://oauth.vk.com/authorize?client_id=${clientId}&redirect_uri=${redirect_uri_auth}&scope=12&display=mobile`,
                    'Auth',
                    `scrollbars=no,resizable=no,status=no,location=no,toolbar=no,menubar=no,width=800,height=500`

                );

                (() => {

                    return new Promise((resolve, reject) => {
                        
                        setTimeout(() => reject('Auth error'), 60000);

                        let awaiterInterval = setInterval(() => {

                            //todo: строковые константы лучше бы вынести (authBearerToken и authRefreshToken)
                            let authBearerToken = localStorage.getItem('authBearerToken');
                            let authRefreshToken = localStorage.getItem('authRefreshToken');
                            
                            if (!!authBearerToken && !!authRefreshToken) {

                                clearInterval(awaiterInterval);
                                return resolve();

                            }

                        }, 500);

                    });

                })().then(() => {

                    alert('Успешная авторизация. Смотри localStorage.')

                }).catch((reason) => {

                    alert(reason)

                });

            }}

        />

    </div>

};