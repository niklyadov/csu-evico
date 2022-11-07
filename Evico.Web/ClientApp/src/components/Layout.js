import React from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export function Layout(props) {

  return <div>
    <NavMenu />
    <Container className='div-window'>
      {props.children}
    </Container>
  </div>

};