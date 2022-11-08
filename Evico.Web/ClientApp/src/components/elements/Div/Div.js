export function Div(props) {

    return <div id={props.id}/>

};
export function DivGrid(props) {

    return <Div className='grid' {...props}/>;

};
export function DivFlex(props) {

    return <Div className='flex' {...props}/>;

};