import React from 'react'
import '.././styles/SlideBar.css';
function SlideBar(props) {

  function scroll() {
    // Pobierz odniesienie do elementu formularza
    const formElement = document.getElementById('shipping-form');
  
    if (formElement) {
      // Oblicz odległość od górnej krawędzi strony do formularza
      const offsetTop = formElement.offsetTop;
  
      // Przewiń stronę do wyliczonej odległości
      window.scrollTo({
        top: offsetTop,
        behavior: 'smooth' // Użyj płynnego przewijania
      });
    }
  }
  return (
    <div className="SlideBar">
        <div className='Counter'>{props.Count} People Online</div>
        <button className='Button' onClick={scroll}>Wypełnij Zgłoszenie</button>
    </div>
  );
}

export default SlideBar;