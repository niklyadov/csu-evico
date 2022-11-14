import Devide from "../Devide";

/**
 * @typedef TDevideFolder
 * @prop {[string]} items
 * @param {import("../Devide").TDevide} props
*/
export default function DevideFolder(props) {

    return <Devide

        {...props}
        className="div-devide__folder"
        section={<Section {...props} />}

    />;

};

/**
 * @typedef TItem
 * @prop {number} key
 * @prop {string} title
 * @param {TItem} props
*/
function Item(props) {

    return <div className={`div-devide__folder_item ${(props.key % 2 === 0) ? 'list-item__two' : ''}`} key={props.key}>
        {props?.title ?? 'Заголовок'}
    </div>;

};
function List(props) {

    return <div className="div-devide__folder_list">
        {props?.items?.map((e, i) => Item({ ...e, key: i }))}
    </div>;

};
function Section(props) {

    return <div className="div-devide__folder_section">
        <List {...props} />
    </div>;

};