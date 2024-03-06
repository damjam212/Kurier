import React from 'react';
import axios from 'axios';

const mapNumberToString = (number) => {
  switch (number) {
    case 0:
      return 'created';
    case 1:
      return 'pending';
    case 2:
      return 'accepted';
    case 3:
      return 'declined';
    default:
      return 'Unknown';
  }
};

const ShowData = ({ data }) => {
  const handleAccept =async (id) => {

    try {
      const response = await axios.post('https://localhost:7076/api/Offerts/accept', id, {
        headers: {
          'Content-Type': 'application/json',
          // Dodaj inne nagłówki, jeśli są potrzebne
        },
      });

      console.log('Response from server:', response.data);
      // Tutaj możesz obsłużyć odpowiedź od serwera
    } catch (error) {
      console.error('Error processing the request:', error);
      // Tutaj możesz obsłużyć błąd
    }
  };

  const handleReject = async (id) => {
    try {
      const response = await axios.delete('https://localhost:7076/api/Offerts/'+id, {
        headers: {
          'Content-Type': 'application/json',
          // Dodaj inne nagłówki, jeśli są potrzebne
        },
      });

      console.log('Response from server:', response.data);
      // Tutaj możesz obsłużyć odpowiedź od serwera
    } catch (error) {
      console.error('Error processing the request:', error);
      // Tutaj możesz obsłużyć błąd
    }
  };
  return (
    <div >
      <h2>Offers </h2>
      <ul>
        {data.map((o, index) => (
          <li key={index}>
            <p>InquiryId: {o.inquiryId}</p>
            <p>status: {mapNumberToString(o.status)}</p>
            {(o.status===1)?<>            <button className='accept' onClick={()=>handleAccept(o.inquiryId)}>Accept</button>
            <button className='decline' onClick={()=>handleReject(o.inquiryId)}>Decline</button></>:null}

          </li>
        ))}

      </ul>
    </div>
  );
};

export default ShowData;
