import React from 'react'
import RedirectButton from '../components/RedirectButton';
import SlideBar from '../components/SlideBar';
import Text from '../components/Text';
import Filtrs from '../components/Filtrs';
import Delivery from '../components/Delivery'
import ListDelivery from '../components/ListDelivery';

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

function Courier() {
    const count = 0;
    const delivery1 = new Delivery_class('Jan', 'Kowalski', 5, 13, 23, 53, 'adres1', 'adres2', 'fast', false, false, false, false);
    const delivery2 = new Delivery_class('Jan', 'Nowakowski', 5, 13, 23, 53, 'adres1', 'adres2', 'fast', false, false, false, true);

    const list = [delivery1, delivery2];

    return (
        <div>
            <div class="g-signin2" data-onsuccess="onSignIn"></div>
            <SlideBar className='Courier Slide Bar' Count={count !== null ? count : 'Ładowanie...'} ButtonText='Show all delivery' NamePage={'Courier HUB'}></SlideBar>
            <Text text="Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum"></Text>

            <Filtrs className='Filtrs for Courier'> </Filtrs>

            <ListDelivery list_delivery={list} />
            <p></p>
            <RedirectButton ButtonName={'Home Page'} Link={'/'}> </RedirectButton>
            <p></p>
            <RedirectButton ButtonName={'Worker'} Link={'/Worker'}> </RedirectButton>
            <p></p>

        </div>
    )
}

export default Courier