import React from 'react'
import { Link } from "react-router-dom";
import SlideBar from '../components/SlideBar';
import HistoryDelivery from '../components/HistoryDelivery';
import OrderBox from '../components/OrderBox';
import ShippingForm from '../components/ShippingForm';
import Text from '../components/Text';
import  { useState, useEffect } from 'react';
import axios from 'axios';
import '../App.css';
import { GoogleOAuthProvider } from '@react-oauth/google';
import { GoogleLogin, googleLogout,useGoogleLogin } from '@react-oauth/google';


function Home(props) {

  const [ordersData,setOrdersData]=useState(null);
  const [currentUser,setCurrentUser]=useState(null);
  const [isLogged,setIsLogged]=useState(false);
  const [email,setEmail]=useState(null);
  const [roles,setRoles]=useState(null);

  const [resp, setResp] = useState(null);

  const responseMessage = (response) => {


    // Pobranie tokenu JWT z odpowiedzi i ustawienie go w stanie
    if (response && response.credential) {
      console.log(response);
      setResp(response);
    }
  };

const errorMessage = (error) => {
    console.log(error);
};


useEffect(() => {
  if (resp) {
    const token = {Token: resp.credential};
    
      axios.get('https://localhost:7105/api/users').then((response) => {
      console.log(response.data);
    });

  }
}, [resp]);

useEffect(() => {
  refresh();
}, []);

const refresh = () => {
  console.log('Strona została wczytana');
  // axios.get('https://localhost:7105/Auth/get-user-email').then((response) => {
  //   console.log(response.data);
  // });

  const xhr = new XMLHttpRequest();
  xhr.onreadystatechange = function () {
      if (xhr.readyState === XMLHttpRequest.DONE) {
          if (xhr.status === 200) {
              const response = JSON.parse(xhr.responseText);
              console.log(response);
              setIsLogged(true);
              setEmail(response.email);
              setRoles(response.roles);
          } else {
              console.error('Błąd pobierania danych:', xhr.status);
              // Obsługa błędów
          }
      }
  };

  xhr.open('GET', 'https://localhost:7105/Auth/get-user-email');
  xhr.withCredentials = true; // Pozwala na przekazywanie ciasteczek
  xhr.send();
};
  const logOut = () => {
      axios.get('https://localhost:7105/Auth/signout').then((response) => {
          console.log(response.data);
      });
  };

    const getOrders = () => {
        const xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function () {
            if (xhr.readyState === XMLHttpRequest.DONE) {
                if (xhr.status === 200) {
                    const response = JSON.parse(xhr.responseText);
                    console.log(response);
                    setOrdersData(response);
                } else {
                    console.error('Błąd pobierania danych:', xhr.status);
                    // Obsługa błędów
                }
            }
        };

        xhr.open('GET', 'https://localhost:7105/api/Requests/user/orders');
        xhr.withCredentials = true; // Pozwala na przekazywanie ciasteczek
        xhr.send();
    };


  const handleCurrUser = (e) => {
    setCurrentUser( e.target.value);
  };

  useEffect(() => {
  
  }, []);
  useEffect(() => {
    // Ta funkcja zostanie wywołana przy odmontowaniu komponentu
    return () => {
      logOut();
    };
  }, []);
//https://excalidraw.com/?fbclid=IwAR14o09F_NyDl7YaiLysRmpb4xjfWvj3z_FIAm12CTQOoLzpKpv9Qioy0AM#room=231f91bfd63768705334,T6G2bqXZVk-xRmB61LUIyA
  return (
    <>
    {email ?         <Text
      name="logged"
      text={
        "zalogowany " +
        email +
        (roles
          ? " Twoje role: " +
            roles.map((role, index) => {
              return index+1 +". " + role;
            }).join(', ')
          : " Brak przypisanych ról.")
      }
    /> : (<Text  name="logged" text="Nie jestes zalogowany" />
    )}
    <div className='HomePage'>

    {/* <GoogleLogin onSuccess={responseMessage} onError={errorMessage} /> */}
          <a className='loginButton' href="https://localhost:7105/Auth/login-google">Zaloguj z google</a>
      {isLogged ? (<a className='loginButton' href="https://localhost:7105/Auth/logout-google">wyloguj z google</a>):null}    
      <SlideBar Count={props.count !== null ? props.count : 'Ładowanie...'}></SlideBar>
      <div class="g-signin2" data-onsuccess="onSignIn"></div>
      <Text text="Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum"></Text>

      <ShippingForm></ShippingForm>
     {isLogged?<button onClick={getOrders}> Get orders</button> : null} 
     {ordersData ? (<OrderBox orders={ordersData} />
        ) : (null
        )}

      <Link to="Worker">OFFICE WORKER</Link>
      <p></p>
      <Link to="Courier">COURIER</Link>

    </div>
    </>
  );
}

export default Home;