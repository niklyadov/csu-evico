export default function Button(props) {

    return <button id={props.id} className={'button ' + props.className}>{props.children}{props.text}</button>

};

export function ButtonText(props) {

    return <Button className='button-text' {...props}>{props.text}</Button>;

};
export function ButtonSvg(props) {

    return <Button className='button-svg' {...props}>{props.children}</Button>;

};