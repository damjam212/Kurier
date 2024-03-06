import React from 'react';
import  { useState, useEffect } from 'react';
const DataItem = ({ dataitem }) => {

  return (
    <div >
                  <p>length: {dataitem.length}</p>
                  <p>width: {dataitem.width}</p>
                  <p>weight: {dataitem.weight}</p>
                  <p>height: {dataitem.height}</p>
                  <p>DesAddress: {dataitem.destinationAddress}</p>
                  <p>SourceAddress: {dataitem.sourceAddress}</p>
                  <p>Prio: {dataitem.priority}</p>
                  <p>Delivery At the Weekend: {dataitem.delAtWeekend}</p>
    </div>
  );
};

export default DataItem;
