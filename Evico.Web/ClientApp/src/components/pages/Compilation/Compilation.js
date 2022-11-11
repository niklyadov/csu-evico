import Main from "../../elements/Main";
import DevideList from "../../elements/Devide/DevideList/DevideList";
import DevideListItem from "../../elements/Devide/DevideList/DevideListItem";
import Progress from "../../elements/Progress/Progress";
import { ButtonSvg } from "../../elements/Buttons/Button";
import { SvgSetting } from "../../elements/Svg/Svg";

export default function Compilation(props) {

    /** @type {[import("../../elements/Devide/DevideList/DevideListItem").TDevideListItem]} */
    const items = [

        {
            rait: 97,
            watchers: 1241,
            participant: 98,
            title: 'Мероприятие',
        },
        {
            rait: 73,
            watchers: 12,
            participant: 35,
            title: 'Поход'
        },
        {
            rait: 100,
            watchers: 74302,
            participant: 100,
            title: 'Чемпионат'
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

function Stat(props) {

    return <div className="div-devide__list_stat">
        <p className="p-devide__list_watchers">Наблюдателей: {props.watchers ?? 0}</p>
    </div>;

};
function Preview(props) {

    return <div className="div-devide__list_preview">
        <h4 className="div-devide__list_title">{props.title}</h4>
        <Progress className='div-devide__list_rait' procent={props.rait}/>
        <Progress className='div-devide__list_participant' procent={props.participant} color='#ffcb5c'/>
        <Stat {...props}/>
    </div>;

};
function Setting(props) {

    return <div className="div-panel">
        <ButtonSvg><SvgSetting/></ButtonSvg>
    </div>;

};