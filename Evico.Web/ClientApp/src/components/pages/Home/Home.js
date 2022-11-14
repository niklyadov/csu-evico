import Button, { ButtonText } from "../../elements/Buttons/Button";
import Devide from "../../elements/Devide/Devide";
import Main from "../../elements/Main";

export default function Home(props) {

    return <Main id='main-home'>
        <Menu/>
    </Main>;

};

function Menu() {

    return <Devide
        id="div-devide__home"
        header="Меню"
        section={<Activity/>}
    />;

};
function Activity() {

    return <div id='section-home'>
        <List>
            <ButtonText
                text="Добавить Место"
            />
            <ButtonText
                text="Добавить Событие"
            />
        </List>
    </div>;

}
function List (props) {

    return <div id='section-home__list' className="div-panel">
        {props.children}
    </div>

}