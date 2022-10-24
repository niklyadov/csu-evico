import ListItem from "./ListItem";
import { ListGroup } from "reactstrap";

export function List() {

    return <div className='list'>
        <header className='list__header'>Список</header>
        <ListGroup className='list__ul'>
            <ListItem></ListItem>
            <ListItem></ListItem>
        </ListGroup>
    </div>

};