import React from 'react';
import { ListGroup } from 'react-bootstrap'; // Importuj ListGroup z react-bootstrap lub innego modułu UI
import { useState, useRef } from "react";
import { Link } from 'react-router-dom';

const OptionList = () => {

    const [isListVisible, setListVisible] = useState(false);
    //const imageRef = useRef(false);
    const handleClick = () => {
        setListVisible(!isListVisible);
    };

    return (

        <div style={{ display: 'flex', alignItems: 'center' }}>
            <img
                src={process.env.PUBLIC_URL + '/icons/option_list.png'}
                alt="Option List Icon"
                width={50}
                height={50}
                //ref={imageRef}
                onClick={handleClick}
                style={{ marginRight: '10px', cursor: 'pointer' }} // Dodaj margines, aby oddzielić ikonę od innych elementów
            />

            {isListVisible ?
                <ul
                    style={{
                        listStyleType: 'none',
                        position: 'absolute',
                        left: '-0%', // Ustawia listę w połowie szerokości obrazka
                        bottom: '-180%',
                        transform: 'translateX(-60%)', // Przesuwa listę w lewo o połowę jej szerokości
                        marginTop: '5px', // Daje odstęp między obrazkiem a listą
                    }}
                >
                    <li className='li_item1'
                        style={{
                            backgroundColor: 'yellow', // Kolor tła
                            width: '200px', // Szerokość listy
                        }}>
                        <Link to="/link1">Opcja 1</Link>
                    </li>

                    <li className='li_item2'
                        style={{

                            backgroundColor: 'yellow', // Kolor tła
                            width: '200px', // Szerokość listy
                        }}>
                        <Link to="/link1">Opcja 2</Link>
                    </li>

                    <li className='li_item3'
                        style={{
                            backgroundColor: 'yellow', // Kolor tła
                            width: '200px', // Szerokość listy
                        }}>
                        <Link to="/link1">Opcja 3</Link>
                    </li>
                </ul>
                : null
            }
        </div>
    );
};

export default OptionList;