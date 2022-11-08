import config from "../../config";

export default function Header(props) {

    return <header id='header-attachment'>

        <section id='section-navigate'>
            <a href={"compilation"}><h5>Подборка</h5></a>
            <a href={"auth"}><h5>Авторизация</h5></a>
            <a href={"test"}><h5>Главная</h5></a>
        </section>
        <h3
            id='h3-brand'
            onClick={_ => window.location.href = config.host + 'auth' }
        >
            <a id='a-brand' href={"auth"}>Месточка</a>
        </h3>

    </header>

};