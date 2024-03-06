import React from 'react';
import  { useState, useEffect } from 'react';
import DataItem from './DataItem';

const Info = ({ info,id }) => {
    const [data, setData] = useState(null);
    const [show, setShow] = useState(false);
    const handleClick =async ( id) => {
        setShow(!show);
        const xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function () {
            if (xhr.readyState === XMLHttpRequest.DONE) {
                if (xhr.status === 200) {
                    const response = JSON.parse(xhr.responseText);
                    setData(response);
                    console.log(response);
                } else {
                    console.error('Błąd pobierania danych:', xhr.status);
                    // Obsługa błędów
                }
            }
        };
      
        xhr.open('GET', 'https://localhost:7105/api/Requests/'+id);
        xhr.withCredentials = true; // Pozwala na przekazywanie ciasteczek
        xhr.send();
      };
  return (
    <div  className='order' >
          <p>id: {info.orderId}</p>
            <p>isReady: {info.isR ? "tak":"nie"}</p>
          <p>length: {info.length}</p>
          <p>width: {info.width}</p>
          <p>height: {info.height}</p>
          <p>weight: {info.weight}</p>
        {/* {data&&show ? ( <DataItem dataitem={data} />) : null} */}
    </div>
  );
};

export default Info;
