import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';

const Notification = ({ message, show }) => {
  const [visible, setVisible] = useState(show);

  useEffect(() => {
    if (show) {
      setVisible(true);
      const timer = setTimeout(() => {
        setVisible(false);
      }, 5000);
      return () => clearTimeout(timer);
    }
  }, [show]);

  return (
    visible && (
        <div className="alert alert-light position-fixed end-0 m-3 shadow-lg " role="alert" style={{ borderRadius: '30px', bottom: '60px', fontFamily: 'Arial, sans-serif', fontSize: '1.2rem' }}>
            {message}
        </div>
    )
  );
};

export default Notification;