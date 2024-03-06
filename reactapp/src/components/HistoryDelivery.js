import React from 'react';

const DeliveryHistory = ({ orders }) => {
  return (
    <div>
      <h2>Delivery History</h2>
      <ul>
        {orders.map((order, index) => (
          <li key={index}>
            <strong>Order {order.index}</strong>
            <p>length: {order.length}</p>
            <p>width: {order.width}</p>
            <p>height: {order.height}</p>
            <p>DesAddress: {order.desAddress}</p>
            <p>SourceAddress: {order.sourceAddress}</p>
            <p>Prio: {order.prio}</p>
            <p>Delivery At the Weekend: {order.DelAtWeekend}</p>
            <p>Status: {order.status}</p>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default DeliveryHistory;
