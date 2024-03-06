import React, { useState, Component } from 'react';
import CheckBox from './CheckBox';

import '.././styles/MakeInquiry.css';

class Field_box_class {
    constructor(name, text, type) {
        this.name = name;
        this.text = text;
        this.type = type;
    };
};


const MakeInquiry = () => {

    const [textFieldsArray, setTextFieldsArray] = useState([
        new Field_box_class('length', 'for example: 115 (in cm)', 'value'),
        new Field_box_class('width', 'for example: 50 (in cm)', 'value'),
        new Field_box_class('height', 'for example: 30 (in cm)', 'value'),
        new Field_box_class('weight', 'for example: 50 (in kg)', 'value'),
        new Field_box_class('destination address', 'for example: ul. Noakowskiego 26/10 Warszawa 00-688', 'text'),
        new Field_box_class('destination address', 'for example: ul. Gwiazdowskiego 5 Warszawa 00-123 Pęcice', 'text'),
    ]); // Stan do przechowywania tekstu w polach



    return (
        <div classname="orange_box_outer">
            <div classname="orange_box_inner">
                {textFieldsArray.map((item, index) => (
                    <div key={index} className="orange-border">
                        <div id="main-text">{item.name}</div>

                        <div key={index} className="Text-field">
                            <input
                                type={item.type}
                                className="place_holder"
                                placeholder={item.text}
                                value={null}
                            />
                        </div>
                    </div>
                ))}

                <CheckBox className="check_box_priority"></CheckBox>
            </div>

        </div>
    );
};

export default MakeInquiry;
