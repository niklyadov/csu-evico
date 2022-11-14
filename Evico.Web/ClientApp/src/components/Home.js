import React, { useState } from 'react';
import { Button, Form, FormGroup, Input, Label } from 'reactstrap';
import { createPlace } from '../scripts/fetch/place';
import { Place } from './classes/Place';

export function Home(props) {

    return <div>

        <FormPlaceCreate/>

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
                console.log(place);
                createPlace(place);
                setPlace(new Place({}));
            }}
        >
            Создать
        </Button>
    </Form>;

};