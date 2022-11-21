import { useState } from "react";
import { ButtonText } from "../../Buttons/Button";
import Devide from "../Devide";
import DevideList from "../DevideList/DevideList";

/**
 * @typedef TDevideFolder
 * @prop {Array<TFolder>} folders
 * @param {import("../Devide").TDevide&TDevideFolder} props
*/
export default function DevideFolder(props) {

    const [index, setIndex] = useState(0);

    return <Devide

        {...props}
        className="div-devide__folder"
        section={<Section
            list={props.folders.map((f, key) => Item({ key, ...f, onClick: _ => changeFolder(key) }))}
            inner={Inner({ inner: props.folders[index].inner })}
        />}

    />;

    function changeFolder(i) {

        if (i === index) return;
        
        setIndex(i);

    };

};

/**
 * @typedef TFolder
 * @prop {number} key
 * @prop {string} title
 * @prop {DevideList} list
 * @param {TFolder} props
*/
function Item(props) {

    return <div className={`div-devide__folder_item ${(props.key % 2 === 0) ? 'list-item__two' : ''}`} key={props.key} onClick={props.onClick} >
        {props?.title ?? 'Заголовок'}
    </div>;

};
function List(props) {

    return <div className="div-devide__folder_list">
        {props?.list}
    </div>;

};
/**
 * @typedef TInner
 * @prop {JSX.Element} inner
 * @param {TInner} props
*/
function Inner(props) {

    return <section
        style={{

            gridArea: 's',

        }}
    >
        {props.inner}
    </section>;

};
/**
 * @typedef TSection
 * @prop {Array<JSX.Element>} list
 * @param {TSection} props
*/
function Section(props) {

    return <div className="div-devide__folder_section">
        <List {...props}/>
        {props.inner}
    </div>;

};