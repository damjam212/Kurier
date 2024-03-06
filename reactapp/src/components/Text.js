import React from 'react'

function Text(props) {
  return (
    <div className={props.name ? props.name:'About'}>
      <p>{props.text}</p>
    </div>
  );
}

export default Text;