import React from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import Auth from './components/pages/Auth/Auth';
import { Home } from './components/pages/Home';

import './custom.css'
import './scripts/document.js';

export default function App() {

  return <Layout>
    <Route exact path='/' component={Home} />
    <Route exact path='/auth' component={Auth} />
  </Layout>

};