import React from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import Auth from './components/pages/Auth/Auth';
import { Home } from './components/pages/Home';

import './custom.css'
import './scripts/document.js';
import AuthVkCallback from "./components/pages/Auth/AuthVkCallback";

export default function App() {

  return <Layout>
    <Route exact path='/' component={Home} />
    <Route exact path='/auth' component={Auth} />
    <Route exact path='/auth/vk-callback' component={AuthVkCallback} />
  </Layout>

};