import Footer from "./Footer";
import Header from "./Header";
import Window from "./Window";
import { useState } from "react";

export default function Layout(props) {

    const [hideHeader] = useState(false);
    const [hideFooter] = useState(false);

    return <div id='layout-attachment'>

        {hideHeader ? null : <Header/>}
        <Window>{props.children}</Window>
        {hideFooter ? null : <Footer/>}

    </div>;

};