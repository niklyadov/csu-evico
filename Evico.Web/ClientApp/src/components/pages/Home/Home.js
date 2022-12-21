import Button, { ButtonText } from "../../elements/Buttons/Button";
import Devide from "../../elements/Devide/Devide";
import DevideFolder from "../../elements/Devide/DevideFolder/DevideFolder";
import DevideList from "../../elements/Devide/DevideList/DevideList";
import Main from "../../elements/Main";

export default function Home(props) {

    return <Main id='main-home'>
        <ListAction/>
        <List/>
    </Main>;

};

function List(props) {

    return <DevideFolder
        style={{

            margin: '0.25em',
            gridArea: 'l',

        }}
        header='Мой Список'
        folders={[

            {
                title: 'Места',
                inner: <div className="div-list text-left-margin">
                    <ItemPlace title='Бургер Кинг' />
                    <ItemPlace title='Ашан' />
                    <ItemPlace title='Родник' />
                </div>
            },
            {
                title: 'Мероприятия',
                inner: <div className="div-list text-left-margin">
                    <ItemEvent title='Вечеринка'/>
                    <ItemEvent title='Олимпиада'/>
                </div>

            },

        ]}
    />;

};
function ListAction(props) {

    return <DevideList
        header="Действия"
        style={{

            margin: '0.25em',
            gridArea: 'a',

        }}>
            <ButtonText text="Добавить место" onclick={_ => window.location = 'create_place'} />
            <ButtonText text="Добавить событие" onclick={_ => window.location = 'create_event'} />
    </DevideList>;

};

function ItemPlace(props) {

    return <div>
        {props.title}
    </div>;

};
function ItemEvent(props) {

    return <div>
        {props.title}
    </div>

};