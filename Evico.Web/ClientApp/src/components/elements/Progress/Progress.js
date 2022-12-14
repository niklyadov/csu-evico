/**
 * @typedef TProgress
 * @prop {number} procent
 * @prop {string} icon
 * @prop {string} color
 * @prop {string} colorEmpty
 * @prop {string} description
 * @param {TProgress} props
*/
export default function Progress(props) {

    const {

        color,
        colorEmpty,

    } = props;

    return <div className={"svg-progress " + props.className}>

        <svg

            xmlns="http://www.w3.org/2000/svg"

        >

            <g>

                <rect className="svg-progress__empty"

                    rx="10px"
                    ry="10px"
                    fill={(colorEmpty) ? colorEmpty : '#cfcfcf'}

                />

            </g>
            <g>

                <rect className="svg-progress__fill"

                    rx="5px"
                    ry="5px"
                    fill={(color) ? color : '#fa8072'}
                    width={(props.procent ?? 0) + "%"}

                />

            </g>

        </svg>

    </div>

};