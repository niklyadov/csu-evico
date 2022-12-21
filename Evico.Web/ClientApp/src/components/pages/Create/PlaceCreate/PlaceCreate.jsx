import { YMaps, Map } from "react-yandex-maps";
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
                        
                    </SC.FieldTag>

                </SC.Form>

                <SC.Panel className="div-panel">
                    <SC.Button>Создать</SC.Button>
                    <SC.Button type='button' onclick={_ => window.location = ''}>Отменить</SC.Button>
                </SC.Panel>

            </div>}
        />
    </SC.PlaceCreateMain>;

};