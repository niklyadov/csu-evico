import Main from "../../elements/Main";
import DevideList from "../../elements/Devide/DevideList/DevideList";
import DevideListItem from "../../elements/Devide/DevideList/DevideListItem";
import Button from "../../elements/Buttons/Button";
import Progress from "../../elements/Progress/Progress";

export default function Compilation(props) {

    /** @type {[import("../../elements/Devide/DevideList/DevideListItem").TDevideListItem]} */
    const items = [

        {
            rait: 97,
            participant: 98,
            title: 'Мероприятие',
        },
        {
            rait: 56,
            participant: 14,
            title: 'Олимпиада'
        },
        {
            rait: 43,
            participant: 65,
            title: 'Поход'
        },
        {
            rait: 37,
            participant: 9,
            title: 'Чемпионат'
        },
        {
            rait: 69,
            participant: 79,
            title: 'Список'
        },
        {
            rait: 16,
            participant: 88,
            title: 'Праздник'
        },
        {
            rait: 76,
            participant: 45,
            title: 'Выборы'
        },
        {
            rait: 63,
            participant: 87,
            title: 'Дегустация'
        },

    ];

    return <Main id='main-compilation'>
        <DevideList
            id='div-devide__compilation'
            header='Подборка'
            footer={<Setting />}
            section={<div className="div-list">{items.map((item, index) => DevideListItem({ ...item, key: index, preview: <Preview {...item}/> }))}</div>}
        />
    </Main>

};

function Preview(props) {

    return <div className="div-devide__list_preview">
        <h4 className="div-devide__list_title">{props.title}</h4>
        <Progress className='div-devide__list_rait' procent={props.rait}/>
        <Progress className='div-devide__list_participant' procent={props.participant} color='#ffcb5c'/>
        <div></div>
    </div>;

};
function Setting(props) {

    return <div className="div-panel">
        <Button text='Настройка' />
    </div>;

};