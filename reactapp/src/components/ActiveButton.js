import React, { useRef } from 'react';
import '.././styles/ActiveButton.css';
import MakeInquiry from './MakeInquiry';

const ActiveButton = ({ ButtonName }) => {
    const targetRef = useRef(null);

    const handleButtonClick = () => {
        // Scrolluj do elementu referencyjnego (targetRef.current)
        targetRef.current.scrollIntoView({ behavior: 'smooth' });
    };

    return (
        <div>
            <div className="outer-container">
                <div className="box"></div>
                <div ref={targetRef} className="box"></div>
                {/* Dodaj więcej divów według potrzeb */}
            </div>
            <button onClick={handleButtonClick}> {ButtonName} </button>
        </div>
    );
};

export default ActiveButton;