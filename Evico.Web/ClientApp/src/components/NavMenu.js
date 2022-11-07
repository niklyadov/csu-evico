import React, { useState } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';

export function NavMenu() {

    const [isOpen, setIsOpen] = useState(false);

    const toggle = () => setIsOpen(!isOpen);

    return <header id='header'>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
            <Container>
                <NavbarBrand tag={Link} to="/" className='a-brand'>Местечко и Точка</NavbarBrand>
                <NavbarToggler onClick={toggle} className="mr-2" />
                <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={isOpen} navbar>
                    <ul className="navbar-nav flex-grow">
                        <NavItem>
                            <NavLink tag={Link} to="/test">Тест</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={Link} to="/compilation">Подбор</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={Link} to="/auth">Авторизация</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={Link} to="/">Главная</NavLink>
                        </NavItem>
                    </ul>
                </Collapse>
            </Container>
        </Navbar>
    </header>;

};