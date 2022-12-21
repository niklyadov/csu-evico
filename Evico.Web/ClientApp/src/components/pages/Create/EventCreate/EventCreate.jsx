import { createEvent } from "../../../../scripts/fetch/event";
import Button, { ButtonText } from "../../../elements/Buttons/Button";
import Devide from "../../../elements/Devide/Devide";
import * as SC from './styles';

export const EventCreate = ({ }) => {

    return <SC.EventCreateMain className="main">
        <Devide
            header="Событие"
            style={{ gridArea: 'f', }}
            section={<div style={{

                height: `100%`,

            }}>
                <SC.Form onSubmit={_ => false}>

                    <SC.FieldHeader>

                        <label><h4>Название</h4></label>
                        <SC.FormInput type='text' placeholder="Классное Событие" />

                    </SC.FieldHeader>

                    <SC.FieldDescription>
                        <label><h4>Описание</h4></label>
                        <SC.Description placeholder="Идеальное событие, хороший тамада и конкурсы интересные!"></SC.Description>
                    </SC.FieldDescription>

                    <SC.FieldTag>
                        <SC.TagNew>
                            <label><h4>Теги</h4></label>
                            <div style={{
                                width: '100%',
                            }}>
                                <ButtonText style={{ borderRadius: '100px', width: '18%' }} onclick={_ => {

                                    const e = document.createElement('label');
                                    const i = document.querySelector('#tagInput');

                                    if (i.value) {

                                        e.classList.add('tag');
                                        e.textContent = i.value;
                                        i.value = '';

                                        document.querySelector('#tags').append(e);

                                    };

                                }}>+</ButtonText>
                                <SC.FormInput id='tagInput' placeholder="Весна" style={{ marginLeft: '0.2em', }}></SC.FormInput>
                            </div>
                            <SC.Tags id='tags'></SC.Tags>
                        </SC.TagNew>
                    </SC.FieldTag>

                </SC.Form>

                <SC.Panel className="div-panel">
                    <SC.Button onclick={_ => {
                        let form = document.querySelector("form");
                        let eventName = form.querySelectorAll("input")[0].value;
                        let eventDescription = form.querySelector("textarea").value;
                        let eventTags = form.querySelectorAll("input")[1].value;
                        createEvent({name: eventName, description: eventDescription, tags: eventTags});
                    }}>Создать</SC.Button>
                    <SC.Button type='button' onclick={_ => window.location = ''}>Отменить</SC.Button>
                </SC.Panel>

            </div>}
        />
    </SC.EventCreateMain>;

};