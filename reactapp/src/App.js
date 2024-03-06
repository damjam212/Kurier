import './App.css';
import { Routes, Route } from "react-router-dom"
import Home from "./pages/Home"
import Worker from "./pages/Worker"
import Courier from "./pages/Courier"
import React, { useState, useEffect } from 'react';
import axios from 'axios';



function App() {

  const [count, setCount] = useState(null);
  useEffect(() => {
    // Funkcja do inkrementacji liczby użytkowników po wejściu na stronę
    const increaseCount = async () => {
      try {
          const responsePost = await axios.post('https://localhost:7105/api/PeopleCounter/increment');
        console.log(responsePost.data);

        setCount(responsePost.data);

      } catch (error) {
        console.error('Błąd podczas inkrementacji i pobierania liczby użytkowników:', error);
      }
    };

    // Wywołanie inkrementacji i aktualizacji liczby użytkowników
    increaseCount();
  }, []);

  useEffect(() => {
    // Funkcja wywoływana przy opuszczeniu strony - zmniejszenie liczby użytkowników
    const decreaseCount = async () => {
      try {
          await axios.post('https://localhost:7105/api/PeopleCounter/decrement');
        // Zmniejszenie liczby użytkowników po wyjściu ze strony
        setCount(prevCount => (prevCount !== null && prevCount > 0) ? prevCount - 1 : 0);
      } catch (error) {
        console.error('Błąd podczas zmniejszania liczby użytkowników:', error);
      }
    };

    // Dodanie nasłuchiwania na zdarzenie opuszczenia strony
    window.addEventListener('beforeunload', decreaseCount);

    // Usunięcie nasłuchiwania zdarzenia po odmontowaniu komponentu
    return () => {
      window.removeEventListener('beforeunload', decreaseCount);
    };
  }, []);
  return (
    <div className="App">

      <Routes>
        <Route path="/" element={ <Home count={count}/> } />
        <Route path="Worker" element={ <Worker/> } />
        <Route path="Courier" element={ <Courier/> } />
      </Routes>
    </div>

  
  );
}

export default App;
