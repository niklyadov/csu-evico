import React, { useState } from 'react';
import { Button, Form, FormGroup, Input, Label } from 'reactstrap';
import { createEvent } from '../scripts/fetch/event';
import { createPlace } from '../scripts/fetch/place';
import { Event } from './classes/Event';
import { Place } from './classes/Place';

export function Home(props) {

    return <div>

        <FormPlaceCreate/>
        <FormEventCreate/>

    </div>;

};

function FormPlaceCreate(props) {

    const [place, setPlace] = useState(new Place({}));

    return <Form>
        <Label>Новое место</Label>
        <FormGroup>
            <Label>Название</Label>
            <Input id='placeName' name='place' placeholder='Укажите название Вашего Места' type='text' onChange={e => setPlace({ ...place, name: e.target.value })}/>
        </FormGroup>
        <FormGroup>
            <Label>Описание</Label>
            <Input id='placeDescription' name='place' placeholder='Опишите Ваше Событие' type='textarea' onChange={e => setPlace({ ...place, description: e.target.value })} />
        </FormGroup>
        <Button
            onClick={_ => {
                createPlace(place);
                setPlace(new Place({}));
            }}
        >
            Создать
        </Button>
    </Form>;

};
function FormEventCreate(props) {

    const [event, setEvent] = useState(new Event({}));

    return <Form>
        <Label>Новое событие</Label>
        <FormGroup>
            <Label>Название</Label>
            <Input id='eventName' name='event' placeholder='Укажите название Вашего События' type='text' onChange={e => setEvent({ ...event, name: e.target.value })}/>
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
                setEvent(new Event({}));
            }}
        >
            Создать
        </Button>
    </Form>;

};