import DevideListItemPreview from "./DevideListItemPreview";

/**
 * @typedef TDevideListItem
 * @prop {number} key
 * @prop {string} title
 * @prop {string} imgUrl
 * @prop {JSX.Element} preview
 * @param {TDevideListItem} props
*/
export default function DevideListItem(props) {

    return <div className={`div-devide__list_item ${props.key % 2 !== 0 ? 'list-item__two' : ''}`} key={props.key}>
        <DevideListItemPreview title={props.title} preview={props.preview} />
    </div>;

};