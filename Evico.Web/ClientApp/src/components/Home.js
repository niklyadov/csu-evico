import React from 'react';
import { Button, Form, FormGroup, Input, Label } from 'reactstrap';

export function Home(props) {

    return <div>

        <Form>
            <Label>Новое мероприятие</Label>
            <FormGroup>
                <Label>Название</Label>
                <Input id='placeName' name='place' placeholder='Укажите название Вашего Места' type='text' />
            </FormGroup>
            <FormGroup>
                <Label>Описание</Label>
                <Input id='placeDescription' name='place' placeholder='Опишите Ваше Событие' type='textarea' />
            </FormGroup>
            <Button>
                Создать
            </Button>
        </Form>

    </div>;

};