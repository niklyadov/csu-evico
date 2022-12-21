/**
 * @typedef TButton
 * @prop {function} onclick
 * @param {TButton} props
*/
export default function Button(props) {

    return <button id={props.id} className={'button ' + props.className} onClick={props.onclick}>{props.children}{props.text}</button>

};
/**
 * @typedef TButtonText
 * @prop {string} text
 * @param {TButtonText&TButton} props
*/
export function ButtonText(props) {

    return <Button className='button-text' {...props}></Button>;

};
export function ButtonSvg(props) {

    return <Button className='button-svg' {...props}>{props.children}</Button>;

};