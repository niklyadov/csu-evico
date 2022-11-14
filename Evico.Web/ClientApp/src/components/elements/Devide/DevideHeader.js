export default function DevideHeader(props) {

    return <header className="div-devide__header">
        <h3>{props.header ?? 'Заголовок'}</h3>
    </header>;

};