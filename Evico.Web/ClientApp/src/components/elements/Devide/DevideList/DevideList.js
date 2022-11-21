import Devide from "../Devide";

/**
 * @param {import("../Devide").TDevide} props
*/
export default function DevideList(props) {

    return <Devide {...props} className="div-devide__list" section={props.section ?? <div className='div-list'>{props.children}</div>} />;

};