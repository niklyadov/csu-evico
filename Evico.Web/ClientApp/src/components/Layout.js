import React from 'react';
import { Container } from 'reactstrap';
import { List } from './List';

export function Layout() {

  return <div className='window'>
    <Container className='container'>
      <List></List>
    </Container>
  </div>

};