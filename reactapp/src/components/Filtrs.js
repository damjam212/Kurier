import React, { useState } from 'react';
import '../styles/Filtrs.css';


class Check_box_class {
    constructor(name, text, type, value) {
        this.name = name;
        this.text = text;
        this.type = type;
        this.value = value;
    };
};

const App = () => {
    const [checkboxValues1, setCheckboxValues1] = useState([
        new Check_box_class('1', 'Sort by delivery order', 'text', false),
        new Check_box_class('2', 'Sort by shipment status', 'text', false),
    ]);

    const [checkboxValues2, setCheckboxValues2] = useState([
        new Check_box_class('3', 'parcels delivered', 'text', false),
        new Check_box_class('4', 'packages that cannot be delivered', 'text', false),
        new Check_box_class('5', 'parcels without status', 'text', false),
    ]);

    const [rectangleColor, setRectangleColor] = useState('orange');


    const handleCheckboxChange1 = (index) => {
        const newCheckboxValues = [...checkboxValues1];
        newCheckboxValues[index] = !newCheckboxValues[index];
        setCheckboxValues1(newCheckboxValues);

        const newRectangleColor = newCheckboxValues.some((value) => value) ? 'red' : 'orange';
        setRectangleColor(newRectangleColor);
    };

    const handleCheckboxChange2 = (index) => {
        const newCheckboxValues = [...checkboxValues1];
        newCheckboxValues[index] = !newCheckboxValues[index];
        setCheckboxValues1(newCheckboxValues);

        const newRectangleColor = newCheckboxValues.some((value) => value) ? 'red' : 'orange';
        setRectangleColor(newRectangleColor);
    };

    return (
        <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'center', height: '50vh', padding: '10px', paddingBottom: '30px' }}>
            {/* Checkboxy i Prostokąt w ramce pomarańczowej */}
            <div style={{ border: '3px solid orange', borderRadius: '10px', padding: '50px', paddingRight: '100px', paddingLeft: '100px', display: 'flex' }}>
                {/* Checkboxy po lewej stronie */}
                <div style={{ fontSize: '20px', padding: '10px' }}>
                    <div style={{ marginRight: '100px', paddingBottom: '10px' }}>Sortowanie:</div>
                    <div style={{ display: 'flex', flexDirection: 'column', marginRight: '20px' }}>
                        {checkboxValues1.map((value, index) => (
                            <div key={index} style={{ paddingRight: '50px', marginBottom: '10px', display: 'flex', alignItems: 'center' }}>
                                <div style={{ width: '20px', height: '20px', backgroundColor: 'orange', marginRight: '10px', border_radius: '10px' }}>
                                    <input
                                        type="checkbox"
                                        checked={value}
                                        onChange={() => handleCheckboxChange1(index)}
                                        style={{ visibility: 'hidden' }}
                                    />
                                </div>
                                {value.text}
                            </div>
                        ))}
                    </div>
                </div>

                <div style={{ fontSize: '20px', padding: '10px' }}>
                    <div style={{ marginRight: '250px', paddingBottom: '10px' }}>Filtry:</div>
                    <div style={{ display: 'flex', flexDirection: 'column', marginRight: '20px' }}>
                        {checkboxValues2.map((value, index) => (
                            <div key={index} style={{ paddingRight: '50px', marginBottom: '10px', display: 'flex', alignItems: 'center' }}>
                                <div style={{ width: '20px', height: '20px', backgroundColor: 'orange', marginRight: '10px' }}>
                                    <input
                                        type="checkbox"
                                        checked={value}
                                        onChange={() => handleCheckboxChange1(index)}
                                        style={{ visibility: 'hidden' }}
                                    />
                                </div>
                                {value.text}
                            </div>
                        ))}
                    </div>
                </div>

                {/* Prostokąt po prawej stronie */}
                <div style={{ backgroundColor: 'orange', borderRadius: '10px', padding: '10px', maxWidth: '150px' }}>
                    {'Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum'}
                </div>
            </div>
        </div>
    );
};

export default App;
