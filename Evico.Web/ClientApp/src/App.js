import React from 'react';
import { Route } from 'react-router';
import Test from './components/pages/Test/Test';
import Auth from './components/pages/Auth/Auth';
import Home from './components/pages/Home/Home';
import Layout from './components/attachments/Layout';
import Setting from './components/pages/Setting/Setting';
import Compilation from './components/pages/Compilation/Compilation';
import AuthVkCallback from "./components/pages/Auth/AuthVkCallback";

import './custom.css'
import './scripts/document.js';

export default function App() {

  return <Layout>
    <Route exact path='/' component={Home} />
    <Route exact path='/auth' component={Auth} />
    <Route exact path='/test' component={Test} />
    <Route exact path='/setting' component={Setting} />
    <Route exact path='/compilation' component={Compilation} />
    <Route exact path='/auth/vk-callback' component={AuthVkCallback} />
  </Layout>

};