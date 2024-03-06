// Rectangle.js

import React, { useState } from 'react';
import '../styles/Delivery.css'
import Rectangle from './Rectangle';

const Delivery = ({ Delivery_object }) => {
    const [expanded, setExpanded] = useState(false);

    const handleButtonClick = () => {
        setExpanded(!expanded);
    };

    return (
        <div className={`rectangle ${expanded ? 'expanded' : ''}`}>
            <div className="inner-rectangle">

                <div className="text-section">
                    <h2>{Delivery_object.name} : {Delivery_object.surname}</h2>

                    {expanded ?
                        <div className="form-section">
                            <div className="left-side">
                                <Rectangle right={false} top_text_="length" text_={Delivery_object.length_}></Rectangle>
                                <Rectangle right={false} top_text_="height" text_={Delivery_object.height}></Rectangle>
                                <Rectangle right={false} top_text_="destination address" text_={Delivery_object.dest_adress}></Rectangle>
                            </div>
                            <div className="right-side">
                                <Rectangle right={true} top_text_="width" text_={Delivery_object.width}></Rectangle>
                                <Rectangle right={true} top_text_="weight" text_={Delivery_object.weight}></Rectangle>
                                <Rectangle right={true} top_text_="source address" text_={Delivery_object.source_adress}></Rectangle>
                            </div>
                        </div>
                        : null}
                </div>
                <button onClick={handleButtonClick}>Toggle</button>
            </div>
            {expanded ?
                <div className="bottom-section">
                    <input type="checkbox" defaultValue={Delivery_object.delivery_at_weekend ? true : false} id="checkbox1" />
                    <label htmlFor="checkbox1" defaultValue={Delivery_object.delivery_at_weekend ? true : false}  >weekend delivery</label>
                    <input type="checkbox" id="checkbox2" />
                    <label htmlFor="checkbox2">Accept</label>
                    <input type="checkbox" id="checkbox3" />
                    <label htmlFor="checkbox3">Decline</label>
                </div>
                : null}
        </div >

    );
};

export default Delivery;
