import React from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { AuthDevide } from './components/pages/Auth/Auth';

import './custom.css'
import './scripts/document.js';
import AuthVkCallback from "./components/pages/Auth/AuthVkCallback";

export default function App() {

  return <Layout>
    <Route exact path='/' component={Home} />
    <Route exact path='/auth' component={AuthDevide} />
    <Route exact path='/auth/vk-callback' component={AuthVkCallback} />
  </Layout>

};