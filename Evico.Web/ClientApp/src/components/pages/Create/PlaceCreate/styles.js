import { YMaps, Map } from 'react-yandex-maps';
import styled from 'styled-components';
import { ButtonText } from '../../../elements/Buttons/Button';

export const PlaceCreateMain = styled.main`

    grid-template-areas:
    ". . ."
    ". f ."
    ". . .";
    grid-template-rows: 1fr 3fr 1fr;
    grid-template-columns: 1fr 2fr 1fr;

`;
export const Devide = styled.div`

    grid-area: f;
    display: flex;

`;
export const Form = styled.form`

    width: 100%;
    height: 85%;
    display: grid;
    grid-template-areas:
    "h c"
    ". c"
    ". c"
    "d d"
    "t t"

`;
export const Field = styled.div`

    display: flex;
    flex-direction: column;
    text-align: center;
    max-width: max-content;
    justify-content: center;
    padding-inline: 1em;

`;
export const FormInput = styled.input`

    color: #fff;
    text-align: center;
    border-radius: 12px;
    border-width: 0;
    background: var(--color-8);

`;
export const FieldHeader = styled(Field)`

    grid-area: h;

`;
export const FieldDescription = styled(Field)`

    grid-area: d;
    width: 100%;
    max-width: 100%;

`;
export const Description = styled.textarea`

    color: #fff;
    resize: none;
    text-align: center;
    border-width: 0;
    border-radius: 12px;
    background: var(--color-8);

`;
export const Panel = styled.div`

    grid-area: p;
    height: max-content;
    margin-top: 0.5em;
    margin-inline: 1em;
    border-radius: 12px;

`;
export const Button = styled(ButtonText)`



`;
export const FieldTag = styled(Field)`

    width: 100%;
    heiht: max-content;
    max-height: 100%;
    max-width: 100%;
    grid-area: t;

`;
export const TagNew = styled.div`



`;
export const Tags = styled.div`



`;