import React from 'react';
import './CustomButton.css';

function CustomButton({ label, onClick }) {
  return (
    <button className="btn btn-primary m-1 custom-button"  onClick={onClick}>
      {label}
    </button>
  );
}

export default CustomButton;