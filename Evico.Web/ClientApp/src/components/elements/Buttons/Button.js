/**
 * @typedef TButton
 * @prop {string} id
 * @prop {string} text
 * @prop {string} className
 * @param {TButton} props
*/
export default function Button(props) {

    return <button id={props.id} className={'button ' + props.className}>{props.children}{props.text}</button>

};
/** @param {TButton} props */
export function ButtonText(props) {

    return <Button className='button-text' {...props}></Button>;

};
export function ButtonSvg(props) {

    return <Button className='button-svg' {...props}>{props.children}</Button>;

};