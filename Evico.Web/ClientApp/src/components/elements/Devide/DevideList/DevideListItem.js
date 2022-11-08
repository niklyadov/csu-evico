import Progress from "../../Progress/Progress";

/**
 * @typedef TDevideListItem
 * @prop {number} key
 * @prop {string} header
 * @prop {string} imgUrl
 * @param {TDevideListItem} props
*/
export default function DevideListItem(props) {

    return <div className={`div-devide__list_item ${props.key % 2 !== 0 ? 'list-item__two' : ''}`} key={props.key}>

        <div className="div-devide__list_preview">
            <h4 className="div-devide__list_title">{props.header}</h4>
            <Progress/>
            <Progress/>
        </div>

    </div>;

};