import React from 'react'
import RedirectButton from '../components/RedirectButton';
import SlideBar from '../components/SlideBar';
import Text from '../components/Text';
import ShowData from '../components/ShowData';
import axios from 'axios';
import ListDelivery from '../components/ListDelivery';
import Filtrs from '../components/Filtrs'
import '.././styles/RedirectButton.css';
import { Link } from "react-router-dom";
import  { useState, useEffect } from 'react';
class Delivery_class {
    constructor(name, surname, length, width, height, weight, dest_adress, source_adress, priority, package_delivered, package_traveling, package_couldnt_deliver, delivery_at_weekend) {
        this.name = name;
        this.surname = surname;
        this.length_ = length;
        this.width = width;
        this.height = height;
        this.weight = weight;
        this.dest_adress = dest_adress;
        this.source_adress = source_adress;
        this.priority = priority;
        this.package_delivered = package_delivered;
        this.package_traveling = package_traveling;
        this.package_couldnt_deliver = package_couldnt_deliver;
        this.delivery_at_weekend = delivery_at_weekend;
    };
};

function Worker() {
    const [data, setData] = useState(null);
    const count = 0;
    const delivery1 = new Delivery_class('Jan', 'Kowalski', 5, 13, 23, 53, 'adres1', 'adres2', 'fast', false, false, false, false);
    const delivery2 = new Delivery_class('Jan', 'Nowakowski', 5, 13, 23, 53, 'adres1', 'adres2', 'fast', false, false, false, true);

    const [isChecked, setIsChecked] = useState(false);
    const [selectedOption, setSelectedOption] = useState('created');

    const handleCheckboxChange = () => {
      setIsChecked(!isChecked);

      
    };
  
    const handleDropdownChange = (event) => {
      setSelectedOption(event.target.value);
    };


    useEffect(() => {

        var params='?status='+selectedOption;
        if(isChecked==true)
        {
            params =params+ '&sortByDate=true';
        }
        axios.get('https://localhost:7076/api/Offerts'+params).then((response) => {


            setData(response.data);
            console.log(response.data)
          });
      }, [isChecked,selectedOption]);

    const list = [delivery1, delivery2];

    return (
        <div>
            <div class="g-signin2" data-onsuccess="onSignIn"></div>
            <SlideBar className='Worker Slide Bar' Count={count !== null ? count : 'Ładowanie...'} ButtonText='Show all delivery' NamePage={'Office worker HUB'}></SlideBar>
            <Text text="Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum"></Text>
            <div>
      <label>
        Sortowanie po dacie :
        <input
          type="checkbox"
          checked={isChecked}
          onChange={handleCheckboxChange}
        />
      </label>
      <p>Checkbox is {isChecked ? 'checked' : 'unchecked'}.</p>

      <label>
        Select Status:
        <select value={selectedOption} onChange={handleDropdownChange}>
          <option value="created">Created</option>
          <option value="pending">Pending</option>
          <option value="accepted">Accepted</option>
          <option value="declined">Declined</option>
        </select>
      </label>
      <p>Selected Status: {selectedOption}</p>
    </div>
            {/* <Filtrs className='Filtrs for Office Worker'> </Filtrs> */}

            {/* <ListDelivery list_delivery={list} > </ListDelivery> */}
            {data ? (<ShowData data={data}/>
        ) :null}
            <p></p>
            <RedirectButton ButtonName={'Home Page'} Link={'/'}> </RedirectButton>
            <p></p>
            <RedirectButton ButtonName={'Courier'} Link={'/Courier'}> </RedirectButton>
            <p></p>
        </div>
    );
}

export default Worker