export default function Svg(props) {

    return <svg {...props} id={props.id} className={"svg " + props.className}>{props.children}</svg>;

};
export function SvgSetting(props) {

    return <Svg width='5em' height='3em' {...props} viewBox='0, 0, 100, 100'>
        <g className="g g-main g-rotate">
            <g>
                <path
                d="m33.5 20 l-20,30 20,30 35,0 20,-30 -20,-30"
                fill='#fff'
                />
            </g>
            <g>
                <circle cx='50' cy='50' r='15' className='svg-setting__circle'/>
            </g>
        </g>
    </Svg>

};