import React from 'react';
import { Route } from 'react-router';
import Layout from './components/attachments/Layout';
import Auth from './components/pages/Auth/Auth';
import Compilation from './components/pages/Compilation/Compilation';
import { PlaceCreate } from './components/pages/Create/PlaceCreate/PlaceCreate.jsx';
import Home from './components/pages/Home/Home';
import './custom.css'
import './scripts/document.js';
import AuthVkCallback from "./components/pages/Auth/AuthVkCallback";
import { EventCreate } from './components/pages/Create/EventCreate/EventCreate';

export default function App() {

  return <Layout>
    <Route exact path='/' component={Home} />
    <Route exact path='/auth' component={Auth} />
    <Route exact path='/compilation' component={Compilation} />
    <Route exact path='/create_place' component={PlaceCreate} />
    <Route exact path='/create_event' component={EventCreate} />
    <Route exact path='/auth/vk-callback' component={AuthVkCallback} />
  </Layout>

};