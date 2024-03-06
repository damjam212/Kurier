import React from 'react';
import '.././styles/RedirectButton.css';
import { useHistory } from 'react-router-dom';
import { BrowserRouter, Routes, Route, Link, useNavigate } from 'react-router-dom';

const RedirectButton = ({ ButtonName, Link }) => {
    const navigate = useNavigate();

    const handleRedirect = () => {
        // Zmień ten link na dowolny, na który chcesz przekierować użytkownika
        const redirectLink = Link;

        // Otwórz nową kartę z podanym linkiem
        navigate(redirectLink);
        //window.open(redirectLink, '_blank');
    };

    return (
        <button className='redirect_button' onClick={handleRedirect}>
            {ButtonName}
        </button>
    );
};

export default RedirectButton;