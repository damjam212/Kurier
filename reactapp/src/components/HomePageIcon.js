import React from 'react';
import { FaHome } from 'react-icons/fa';
import { Link } from 'react-router-dom';
import '.././styles/HomePageIcon.css';

const HomePageIcon = () => {
    return (
        <Link to="/">
            <div style={{ display: 'flex', alignItems: 'center' }}>
                <img
                    src={process.env.PUBLIC_URL + '/icons/home_page_icon.png'}
                    alt="Home Page Icon"
                    width={50}
                    height={50}
                    style={{ marginRight: '10px', cursor: 'pointer' }} // Dodaj margines, aby oddzielić ikonę od innych elementów
                />
            </div>
        </Link>
    );
};

export default HomePageIcon;