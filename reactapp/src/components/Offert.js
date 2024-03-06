import React from 'react';
import '../styles/Offert.css';

const Offert = ({inquiryId, price,expiringAt,currency,close }) => {
    const handleClick = () => {
       close();
       var xhr = new XMLHttpRequest();
       var data= {inquiryId};
       // Skonfiguruj zapytanie
       xhr.open('POST', 'https://localhost:7105/api/Requests/pick_offer', true);
   
       xhr.setRequestHeader("Content-Type", "application/json");
       xhr.onreadystatechange = function () {
           if (xhr.readyState === 4) {
               // Obsługa odpowiedzi
               if (xhr.status === 200) {
                   console.log('Zapytanie udane:', xhr.responseText);
               } else {
                   console.error('Błąd zapytania:', xhr.status, xhr.statusText);
               }
           }
       };
       // Wyślij zapytanie z danymi formData
       xhr.withCredentials = true; // Pozwala na przekazywanie ciasteczek
   
       xhr.send(JSON.stringify(data));
   
      };

  return (
    <div className="offer" onClick={handleClick}>
      <h2>Oferta</h2>
      <p>Numer zapytania: {inquiryId}</p>
      <p>price: {price} </p>
      <p>waluta: {currency}</p>
      <p>Wygasza: {expiringAt}</p>
    </div>
  );
};

export default Offert;