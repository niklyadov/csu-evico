import Main from "../../elements/Main";
import DevideList from "../../elements/Devide/DevideList/DevideList";
import DevideListItem from "../../elements/Devide/DevideList/DevideListItem";
import DevideListSettings from "../../elements/Devide/DevideList/DevideListSettings";
import Button from "../../elements/Buttons/Button";

export default function Compilation(props) {

    /** @type {[import("../../elements/Devide/DevideList/DevideListItem").TDevideListItem]} */
    const items = [

        { header: 'Мероприятие' },
        { header: 'Олимпиада' },
        { header: 'Поход' },
        { header: 'Чемпионат' },
        { header: 'Список' },

    ];

    return <Main id='main-compilation'>
        <DevideList
            id='div-devide__compilation'
            header='Подборка'
            footer={<Setting/>}
            section={<div className="div-list">{items.map((item, index) => DevideListItem({ ...item, key: index }))}</div>}
        />
    </Main>

};

function Setting(props) {

    return <div className="div-panel">
        <Button text='Настройка'/>
    </div>;

};