import DevideFooter from "./DevideFooter";
import DevideHeader from "./DevideHeader";
import DevideSection from "./DevideSection";

/**
 * @typedef TDevide
 * @prop {string} id
 * @prop {string} header
 * @prop {string} className
 * @prop {[JSX.Element]} children
 * @prop {JSX.Element} section
 * @prop {JSX.Element} footer
 * @param {TDevide} props
*/
export default function Devide(props) {

    return <div id={props.id} className={'div-devide'}>

        <DevideHeader {...props} />
        <DevideSection {...props} />
        <DevideFooter {...props} />

    </div>

};