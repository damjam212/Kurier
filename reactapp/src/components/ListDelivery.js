import React from 'react';
import Delivery from './Delivery';

const ListDelivery = ({ list_delivery }) => {
    // Przykładowa lista przedmiotów


    return (
        <div>
            <h2>Historia zamowień:</h2>
            <ul>
                {list_delivery.map((subject, index) => (
                    <Delivery Delivery_object={subject}></Delivery>
                ))}
            </ul>
        </div>
    );
};

export default ListDelivery;
