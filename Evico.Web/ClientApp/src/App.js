import React from 'react';
import { Route } from 'react-router';
import Layout from './components/attachments/Layout';
import Auth from './components/pages/Auth/Auth';
import Compilation from './components/pages/Compilation/Compilation';
import { PlaceCreate } from './components/pages/Create/PlaceCreate/PlaceCreate.jsx';
import Home from './components/pages/Home/Home';
import './custom.css'
import './scripts/document.js';

export default function App() {

  return <Layout>
    <Route exact path='/' component={Home} />
    <Route exact path='/auth' component={Auth} />
    <Route exact path='/compilation' component={Compilation} />
    <Route exact path='/create_place' component={PlaceCreate} />
  </Layout>

};