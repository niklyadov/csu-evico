import { YMaps, Map } from "react-yandex-maps";
import { createPlace } from "../../../../scripts/fetch/place";
import Button, { ButtonText } from "../../../elements/Buttons/Button";
import Devide from "../../../elements/Devide/Devide";
import * as SC from './styles';

export const PlaceCreate = ({ }) => {

    return <SC.PlaceCreateMain className="main">
        <Devide
            header="Место"
            style={{ gridArea: 'f', }}
            section={<div style={{

                height: `100%`,

            }}>
                <SC.Form onSubmit={_ => false}>

                    <SC.FieldHeader>

                        <label><h4>Название</h4></label>
                        <SC.FormInput type='text' placeholder="Классное Местечко" />

                    </SC.FieldHeader>

                    <SC.FieldDescription>
                        <label><h4>Описание</h4></label>
                        <SC.Description placeholder="Идеальное место для новых знакомств!"></SC.Description>
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
                        let placeName = form.querySelectorAll("input")[0].value;
                        let placeDescription = form.querySelector("textarea").value;
                        let placeTags = form.querySelectorAll("input")[1].value;
                        createPlace({name: placeName, description: placeDescription, tags: placeTags});
                    }}>Создать</SC.Button>
                    <SC.Button type='button' onclick={_ => window.location = ''}>Отменить</SC.Button>
                </SC.Panel>

            </div>}
        />
    </SC.PlaceCreateMain>;

};