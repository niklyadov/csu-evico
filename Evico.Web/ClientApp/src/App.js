import React from 'react';
import { Route } from 'react-router';
import Test from './components/pages/Test/Test';
import Auth from './components/pages/Auth/Auth';
import Main from './components/pages/Main/Main';
import Layout from './components/attachments/Layout';
import Compilation from './components/pages/Compilation/Compilation';
import AuthVkCallback from "./components/pages/Auth/AuthVkCallback";

import './custom.css'
import './scripts/document.js';

export default function App() {

  return <Layout>
    <Route exact path='/' component={Main} />
    <Route exact path='/auth' component={Auth} />
    <Route exact path='/auth/vk-callback' component={AuthVkCallback} />
    <Route exact path='/test' component={Test} />
    <Route exact path='/compilation' component={Compilation} />
  </Layout>

};