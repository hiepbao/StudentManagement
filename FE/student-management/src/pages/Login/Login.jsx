import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faGooglePlusG, faFacebookF, faGithub, faLinkedinIn } from '@fortawesome/free-brands-svg-icons';
import image from '../../assets/images/image.png';
import { loginApi } from '../../services/Auth';
import { saveAuthToken } from '../../services/tokenService';
import './Login.css';

function Login() {
  const [phoneNumber, setPhoneNumber] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate(); // Sử dụng useNavigate để chuyển hướng

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
        navigate('/school'); // Chuyển hướng đến trang /school sau khi đăng nhập thành công
      }
    } catch (error) {
      console.error('Login error:', error);
      alert('Đăng nhập thất bại');
    }
  };

  return (
    <div className="container-fluid d-flex align-items-center justify-content-center" style={{ minHeight: 'calc(100vh - 150px)' }}>
      <div className="row d-flex align-items-stretch">
        <div className="col-12 col-md-6 d-flex align-items-stretch">
          <div className="d-flex align-items-center justify-content-center w-100">
            <img src={image} alt="Database Illustration" className="img-fluid w-100" style={{ borderRadius: '5px', objectFit: 'cover', maxHeight: '100%' }} />
          </div>
        </div>
        <div className="col-12 col-md-6 d-flex align-items-stretch">
          <div className="card p-4 shadow w-100 d-flex align-items-center justify-content-center">
            <form className="w-100 mt-3 mt-md-5" onSubmit={handleLogin}>
              <h3 className="text-center mb-4">Sign In</h3>
              <div className="d-flex justify-content-center mb-3">
                <a href="/auth/google" className="icon me-2">
                  <FontAwesomeIcon icon={faGooglePlusG} />
                </a>
                <a href="/auth/facebook" className="icon me-2">
                  <FontAwesomeIcon icon={faFacebookF} />
                </a>
                <a href="/auth/github" className="icon me-2">
                  <FontAwesomeIcon icon={faGithub} />
                </a>
                <a href="/auth/linkedin" className="icon">
                  <FontAwesomeIcon icon={faLinkedinIn} />
                </a>
              </div>
              <span className="d-block text-center mb-3">or use your phone number and password</span>
              <div className="mb-3">
                <label htmlFor="phoneNumber" className="form-label visually-hidden">Phone Number</label>
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
                <label htmlFor="password" className="form-label visually-hidden">Password</label>
                <input
                  type="password"
                  id="password"
                  className="form-control"
                  placeholder="Password"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                />
              </div>
              <a href="/forgot-password" className="d-block text-end text-primary mb-3">Forget Your Password?</a>
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
