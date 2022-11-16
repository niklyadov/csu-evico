import React, { useEffect, useState } from 'react';
import { Button, Form, FormGroup, Input, Label, ListGroup, ListGroupItem } from 'reactstrap';
import config from '../config';
import { createEvent, deleteEventById, getEventsList } from '../scripts/fetch/event';
import { createPlace, deletePlaceById } from '../scripts/fetch/place';
import { Event } from './classes/Event';
import { Place } from './classes/Place';

export function Home(props) {

    console.log(config);

    return <div>

        <ListPlace places={[{ name: 'Ресторан' }, { name: 'sqwdfdvbfrntgf' }]} />
        <ListEvent events={[{ name: 'Пати' }]} />
        <FormPlaceCreate />
        <FormEventCreate />

    </div>;

};

function PlaceItem(props) {

    return <div className='div-panel' key={props.key}>
        <h4>{props.name}</h4>
        <Button>Изменить</Button>
        <Button
            onClick={deletePlaceById(props.id)}
        >Удалить</Button>
    </div>;

};
function EventItem(props) {

    return <div className='div-panel' key={props.key}>
        <h4>{props.name}</h4>
        <Button>Изменить</Button>
        <Button
            onClick={deleteEventById(props.id)}
        >Удалить</Button>
    </div>;

};
function ListPlace(props) {

    const [list, setList] = useState([]);

    useEffect(() => {

        const l = getEventsList() ?? [];

        setList(l);

    }, []);

    return <ListGroup>
        <Label>Места</Label>
        {props?.places?.map((p, key) =>
            <ListGroupItem key={key}>
                {PlaceItem({ ...p })}
            </ListGroupItem>)}
    </ListGroup>;

};
function ListEvent(props) {

    // const [list, setList] = useState([]);

    // useEffect(() => {

    //     const l = getEventsList() ?? [];

    //     setItems(l);

    // }, []);

    return <ListGroup>
        <Label>Мероприятия</Label>
        {props?.events?.map((e, key) => <ListGroupItem key={key}>{EventItem({...e })}</ListGroupItem>)}
    </ListGroup>;

};
function FormPlaceCreate(props) {

    const [place, setPlace] = useState(new PlaceItem({}));

    return <Form>
        <Label>Новое место</Label>
        <FormGroup>
            <Label>Название</Label>
            <Input id='placeName' name='place' placeholder='Укажите название Вашего Места' type='text' onChange={e => setPlace({ ...place, name: e.target.value })} />
        </FormGroup>
        <FormGroup>
            <Label>Описание</Label>
            <Input id='placeDescription' name='place' placeholder='Опишите Ваше Событие' type='textarea' onChange={e => setPlace({ ...place, description: e.target.value })} />
        </FormGroup>
        <Button
            onClick={_ => {
                createPlace(place);
                setPlace(new PlaceItem({}));
            }}
        >
            Создать
        </Button>
    </Form>;

};
function FormEventCreate(props) {

    const [event, setEvent] = useState(new EventItem({}));

    return <Form>
        <Label>Новое событие</Label>
        <FormGroup>
            <Label>Название</Label>
            <Input id='eventName' name='event' placeholder='Укажите название Вашего События' type='text' onChange={e => setEvent({ ...event, name: e.target.value })} />
        </FormGroup>
        <FormGroup>
            <Label>Описание</Label>
            <Input id='eventDescription' name='event' placeholder='Опишите Ваше Событие' type='textarea' onChange={e => setEvent({ ...event, description: e.target.value })} />
        </FormGroup>
        <FormGroup>
            <Label>Выберите Место</Label>
            <Input id='eventPlaceId' name='event' placeholder='Укажите ID события :)' type='number' onChange={e => setEvent({ ...event, placeId: e.target.value })} />
        </FormGroup>
        <Button
            onClick={_ => {
                createEvent(event);
                setEvent(new EventItem({}));
            }}
        >
            Создать
        </Button>
    </Form>;

};