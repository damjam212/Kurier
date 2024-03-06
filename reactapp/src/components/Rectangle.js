import React from 'react';


const Rectangle = ({ text_, top_text_, right }) => {
    const styles = {

        container: {
            backgroundColor: 'orange', // Jasnopomarańczowe tło
            borderRadius: '10px', // Zaokrąglona ramka
            border: '2px solid darkorange',
            width: '150%',

        },
        text: {
            color: 'black', // Ciemnopomarańczowy kolor tekstu
        },

        top_text: {
            marginBottom: '10px',
            textAlign: 'center',
            //marginLeft: '200px'
        },
    };

    return (

        <div style={styles.top_text}>{top_text_}
            <div style={styles.container}>
                <div style={styles.text}>{text_}</div>
            </div>
        </div>
    );
};

export default Rectangle;
