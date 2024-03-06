import React, { useState } from 'react';

const OrangeCheckbox = () => {
    const [isChecked, setIsChecked] = useState(false);

    const handleCheckboxChange = () => {
        setIsChecked(!isChecked);
    };

    return (
        <label style={{ display: 'flex', alignItems: 'center', justifyContent: 'flex-end', paddingRight: '40%', height: '100px' }}>
            <input
                type="checkbox"
                checked={isChecked}
                onChange={handleCheckboxChange}
                style={{
                    appearance: 'none',

                    width: '50px',
                    height: '50px',
                    backgroundColor: isChecked ? '#FFA500' : '#FF8C00',
                    borderRadius: '5px',
                    marginRight: '8px',
                    cursor: 'pointer',
                }}
            />
            <span style={{ fontSize: '18px', fontWeight: 'bold' }}>delivery at weekend</span>
        </label>
    );
};

export default OrangeCheckbox;
