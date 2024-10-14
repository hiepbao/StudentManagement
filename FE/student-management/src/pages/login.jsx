import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faGooglePlusG, faFacebookF, faGithub, faLinkedinIn } from '@fortawesome/free-brands-svg-icons';
import image from '../assets/images/image.png';
import { loginApi } from '../services/Auth';
import { saveAuthToken } from '../services/tokenService';
import School from '../pages/School';

function Login() {
  const [phoneNumber, setPhoneNumber] = useState('');
  const [password, setPassword] = useState('');
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  const handleLogin = async (e) => {
    e.preventDefault();
    try {
      const response = await loginApi(phoneNumber, password);
      if (response.data && response.data.token) {
        const { token } = response.data;
        saveAuthToken(token.result);
        alert('Đăng nhập thành công');
        try {
          let decodedToken;
            decodedToken = JSON.parse(atob(token.result.split('.')[1].replace(/-/g, '+').replace(/_/g, '/')));
            console.log('Decoded Token:', decodedToken);
        } catch (e) {
          console.error('Failed to decode token:', e);
        }
        setIsLoggedIn(true);
      }
    } catch (error) {
      console.error('Login error:', error);
      alert('Đăng nhập thất bại');
    }
  };

  if (isLoggedIn) {
    return (
      <>
        <School />
      </>
    );
  }

  return (
    <div className="container-fluid d-flex align-items-center justify-content-center" style={{ minHeight: 'calc(100vh - 150px)' }}>
      <div className="row w-100 w-md-75">
        <div className="col-12 col-md-6 d-flex align-items-center justify-content-center mb-4 mb-md-0">
          <img src={image} alt="Database Illustration" className="img-fluid w-75 w-md-100" style={{ borderRadius: '5px' }} />
        </div>
        <div className="col-12 col-md-6 d-flex align-items-center justify-content-center">
          <div className="card p-4 shadow w-100 mx-auto">
            <form className='w-100 mt-3 mt-md-5' onSubmit={handleLogin}>
              <h3 className="text-center mb-4">Sign In</h3>
              <div className="d-flex justify-content-center mb-3">
                <a href="#" className="icon me-2">
                  <FontAwesomeIcon icon={faGooglePlusG} />
                </a>
                <a href="#" className="icon me-2">
                  <FontAwesomeIcon icon={faFacebookF} />
                </a>
                <a href="#" className="icon me-2">
                  <FontAwesomeIcon icon={faGithub} />
                </a>
                <a href="#" className="icon">
                  <FontAwesomeIcon icon={faLinkedinIn} />
                </a>
              </div>
              <span className="d-block text-center mb-3">or use your phone number and password</span>
              <div className="mb-3">
                <input
                  type="text"
                  id="phoneNumber"
                  className="form-control"
                  placeholder="Phone Number"
                  value={phoneNumber}
                  onChange={(e) => setPhoneNumber(e.target.value)}
                />
              </div>
              <div className="mb-3">
                <input
                  type="password"
                  id="password"
                  className="form-control"
                  placeholder="Password"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                />
              </div>
              <a href="#" className="d-block text-end text-primary text-center mb-3">Forget Your Password?</a>
              <div className="d-flex justify-content-center mt-3">
                <button type="submit" className="btn btn-primary w-75 w-md-50">Sign In</button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Login;