import React, { useState } from 'react';
import axios from 'axios';
import '.././styles/ShippingForm.css';
import Modal from 'react-modal';
import Offert from './Offert';
// public int Width { get; set; }
// public int Height { get; set; }
// public int Length { get; set; }
// public string Currency { get; set; }
// public int Weight { get; set; }
// public string Source { get; set; }
// public string Destination { get; set; }
// public DateTime DeliveryDay { get; set; }
// public bool DeliveryInWeekend { get; set; }
// public string Prio { get; set; }

const ShippingForm = () => {
  const [formData, setFormData] = useState({
    Width: 1,
    Height: 1,
    Length: 1,
    Currency: '', // Dla przykładu, może być typu string
    Weight: 1,
    Source: '',
    Destination: '',
    DeliveryDate:new Date()+7, // Może być typu Date
    PickupDate: new Date()+1, // Dodane pole pickupDate
    DeliveryInWeekend: false,
    IsCompany: false,
    Prio: 'low', // Dla przykładu, może być typu string
});
const [webSocket, setWebSocket] = useState(null);
const [info, setInfo] = useState(true);
const [showModal, setShowModal] = useState(false);
const [data, setData] = useState(null);

const handleOpenModal = () => {
  setShowModal(true);
};

const handleCloseModal = () => {
  setInfo(true);
  setShowModal(false);
};

  const handleInputChange = (e) => {
    const value = e.target.type === 'checkbox' ? e.target.checked : e.target.value;
    setFormData({
      ...formData,
      [e.target.name]: value
    });
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

    // console.log('data:', formData);
    // var xhr = new XMLHttpRequest();

    // xhr.open('POST', 'https://localhost:7105/api/Requests', true);

    // xhr.setRequestHeader("Content-Type", "application/json");

    // xhr.onreadystatechange = function () {
    //     if (xhr.readyState === 4) {
    //         if (xhr.status === 200) {
    //             console.log('Zapytanie udane:', xhr.responseText);
    //                   console.log('Response from server:',xhr.response);
    //                   var offerDto = JSON.parse(xhr.responseText);

    //                   setData(offerDto);
    //                   setShowModal(true);
    //         } else {
    //             console.error('Błąd zapytania:', xhr.status, xhr.statusText);
    //             console.log('Response from server:',xhr.response);
    //         }
    //     }
    // };
    // xhr.withCredentials = true;

    // xhr.send(JSON.stringify(formData));



    // -- websocket --

    setInfo(true);
      // Utwórz obiekt WebSocket, podając adres URL serwera WebSocket.
        // Utwórz obiekt WebSocket, podając adres URL serwera WebSocket.
        const socket = new WebSocket('wss://localhost:7105/api/Requests/ws'); // Zastąp adresem URL swoim adresem WebSocket

        // Obsługa otwarcia połączenia
        socket.onopen = () => {
          console.log('Połączono z serwerem WebSocket.');
          socket.send(JSON.stringify(formData));
          setTimeout(() => {
            socket.close();

            console.log('Zamknięcie połączenia po 5 sekundach.');
          }, 5000);
        };
    
        // Obsługa zamknięcia połączenia
        socket.onclose = (event) => {
          setInfo(false);
          console.log(`Zamknięto połączenie WebSocket: ${event.reason}`);
        };
    
        // Obsługa otrzymanych wiadomości
        socket.onmessage = (event) => {
          const json = event.data;
          const receivedData = JSON.parse(json);
          setShowModal(true);

          // Przetworzenie otrzymanych danych
          console.log(receivedData);
          setData(receivedData);
        };
    
        // Zapisz utworzony obiekt WebSocket w stanie komponentu
        setWebSocket(socket);

  };

  return (
    <>
      <Modal
        isOpen={showModal}
        onRequestClose={handleCloseModal}
        contentLabel="Przykładowy Modal"
      >
        <h2>Znalezione oferty:</h2>
        {data ? (
  data.map((offer, index) => (
    <Offert
      key={index}  // Klucz musi być unikalny dla każdego elementu w mapie
      inquiryId={offer.InquiryId}
      price={offer.Price}
      expiringAt={offer.ExpiringAt}
      currency={offer.Currency}
      close={handleCloseModal}
    />
  ))
) : null}
{info ? <p>Pobieranie ofert ...</p>:<p>wszystkie oferty zostały pobrane</p>}
        <button onClick={handleCloseModal}>Zamknij</button>

      </Modal>
    <form onSubmit={handleSubmit} id="shipping-form">
      <label>
        Długość:
        <input display="none" className="NumberLabel" type="number" name="Length" value={formData.length} onChange={handleInputChange}/>
      </label>
      <br />
      <label>
        Szerokość:
        <input  display="none" className="NumberLabel" type="number" name="Width"  value={formData.width} onChange={handleInputChange}/>
      </label>
      <br />
      <label>
        Waga:
        <input  display="none" className="NumberLabel" type="number" name="Weight" value={formData.weight} onChange={handleInputChange}/>
      </label>
      <br />
      <label>
        Wysokość:
        <input className="NumberLabel" type="number" name="Height" value={formData.height} onChange={handleInputChange}/>
      </label>
      <br />
      <label>
        Destination Address:
        <input className="NumberLabel" type="text" name="Destination" value={formData.destinationAddress} onChange={handleInputChange}/>
      </label>
      <br />
      <label>
        Source Address:
        <input className="NumberLabel" type="text" name="Source" value={formData.sourceAddress} onChange={handleInputChange}/>
      </label>
      <br />
      <label>
          Data dostawy:
          <input
            type="datetime-local"
            name="DeliveryDate"
            value={formData.deliveryDate} // Formatowanie na string zgodny z datetime-local
            onChange={handleInputChange}
          />
        </label>
        <br />
        <label>
          Data odebrania:
          <input
            type="datetime-local"
            name="PickupDate"
            value={formData.deliveryDate} // Formatowanie na string zgodny z datetime-local
            onChange={handleInputChange}
          />
        </label>
        <br />
      <label>
        Deliver at the weekend:
        <input type="checkbox" name="DeliveryInWeekend" value={formData.deliverWeekend} onChange={handleInputChange} />
      </label>
      <br />
      <label>
        Is a Company:
        <input type="checkbox" name="IsCompany" value={formData.IsCompany} onChange={handleInputChange} />
      </label>
      <br />
      <label>
        Priority:
        <select name="Prio" value={formData.priority} onChange={handleInputChange}>
          <option value="low">Low</option>
          <option value="medium">Medium</option>
          <option value="high">High</option>
        </select>
      </label>
      <br />

      <input type="submit" value="Submit" />
    </form>
    </>
  );
};

export default ShippingForm;
