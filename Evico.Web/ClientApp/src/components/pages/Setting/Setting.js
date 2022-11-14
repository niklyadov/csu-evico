import Main from "../../elements/Main";
import DevideFolder from "../../elements/Devide/DevideFolder/DevideFolder";

export default function Setting(props) {

    return <Main id='main-setting'>
        <DevideFolder
            id='div-devide__setting'
            header="Настройки"
            items={[

                { title: 'Общее' },
                { title: 'Особые', },
                { title: 'Кастомизация', },

            ]}
        />
    </Main>

};