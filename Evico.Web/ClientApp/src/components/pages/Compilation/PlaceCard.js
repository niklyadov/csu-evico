/**
 * @typedef T
 * @prop {string} header
 * @param {T} props
*/
export default function PlaceCard(props) {

    return <div
    
        className="div-card__place"
        
    >

        <header>
            <h3>{props?.header}</h3>
        </header>
        <section>
            <div>

            </div>
        </section>
        <footer></footer>

    </div>

};