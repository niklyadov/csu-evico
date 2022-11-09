/**
 * @typedef TDevideListItemPreview
 * @prop {string} title
 * @prop {JSX.Element} preview
 * @param {TDevideListItemPreview} props
*/
export default function DevideListItemPreview(props) {

    return (props.preview) ? props.preview : <div className="div-devide__list_preview">
        {props.preview ? props.preview : <h4 className="div-devide__list_title">{props.title}</h4>}
    </div>;

};