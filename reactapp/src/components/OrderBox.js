import React from 'react';
import  { useState, useEffect } from 'react';
import Info from './Info';
const OrderBox = ({ orders }) => {



  return (
    <div>
      <h2>Delivery History</h2>
      <ul>
        {orders.map((order, index) => (
          <div>
             <Info info={order} ></Info>
          </div>
        ))}
      </ul>
    </div>
  );
};

export default OrderBox;
