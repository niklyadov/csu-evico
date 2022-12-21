import Main from "../../elements/Main";
import DevideList from "../../elements/Devide/DevideList/DevideList";
import DevideListItem from "../../elements/Devide/DevideList/DevideListItem";
import Progress from "../../elements/Progress/Progress";
import { ButtonSvg } from "../../elements/Buttons/Button";
import { SvgSetting } from "../../elements/Svg/Svg";
import { useEffect, useState } from "react";
import { YMaps, Map, Placemark } from 'react-yandex-maps';

export default function Compilation(props) {

    /** @type {[import("../../elements/Devide/DevideList/DevideListItem").TDevideListItem]} */

    const [items, setItems] = useState([]);

    const defaultState = {
        center: [55.174567, 61.390343],
        zoom: 12,
    };

    useEffect(() => {

        try {
            setItems([
                {
                    name: "qwe1"
                    ,participant: 100
                    ,watchers: 88005553535
                    ,rait: 1
                },
                {
                    name: "qwe2"
                    ,participant: 10
                    ,watchers: 1111
                    ,rait: 1
                },
                {
                    name: "qwe3"
                    ,participant: 20
                    ,watchers: 123213
                    ,rait: 1
                },
                {
                    name: "rewqty"
                    ,participant: 50
                    ,watchers: 100500
                    ,rait: 1
                }
        ]);

        } catch (e) {

            setItems([]);

        };

    }, []);

    return <Main id='main-compilation'>
        <DevideList
            id='div-devide__compilation'
            header='Подборка'
            footer={<Setting />}
            section={<div className="div-list">{items.map((item, index) => DevideListItem({ ...item, key: index, preview: <Preview {...item} title={item.name} /> }))}</div>}
        />
        <YMaps>
            <Map id='map' defaultState={defaultState} style={{ gridArea: 'm', padding: '1em', paddingTop: '3.25em', paddingBottom: '4em' }}>
                <Placemark geometry={[55.174567, 61.390343]} />
            </Map>
        </YMaps>
    </Main>

};

function Stat(props) {

    return <div className="div-devide__list_stat">
        <p className="p-devide__list_watchers">Подписчики: {props.watchers ?? 0}</p>
    </div>;

};
function Preview(props) {

    return <div className="div-devide__list_preview">
        <h4 className="div-devide__list_title">{props.title}</h4>
        <Progress className='div-devide__list_rait' procent={props.rait} />
        <Progress className='div-devide__list_participant' procent={props.participant} color='#ffcb5c' />
        <Stat {...props} />
    </div>;

};
function Setting(props) {

    return <div className="div-panel">
        <ButtonSvg><SvgSetting /></ButtonSvg>
    </div>;

};