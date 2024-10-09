import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
import Header from './components/Header';
import Nav from './components/Nav';
import Footer from './components/Footer';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import School from './pages/School';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faGooglePlusG, faFacebookF, faGithub, faLinkedinIn } from '@fortawesome/free-brands-svg-icons';
import image from './assets/images/image.png';

function App() {
  return (
    <Router basename="/">
      <div>
        <Header />
        <Nav />
        <Routes>
          <Route path="/" element={
            <div className="container-fluid d-flex align-items-center justify-content-center vh-100">
              <div className="row w-50">
                <div className="col-md-6 d-flex align-items-center justify-content-center">
                  <img src={image} alt="Database Illustration" className="img-fluid w-100" style={{ borderRadius: '5px' }} />
                </div>
                <div className="col-md-6 d-flex align-items-center justify-content-center">
                  <div className="card p-4 shadow w-100 h-100 mx-auto">
                    <form className='w-100 h-100 mt-5'>
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
                      <span className="d-block text-center mb-3">or use your email password</span>
                      <div className="mb-3">
                        <input type="email" id="username" className="form-control" placeholder="Email" />
                      </div>
                      <div className="mb-3">
                        <input type="password" id="password" className="form-control" placeholder="Password" />
                      </div>
                      <a href="#" className="d-block text-end text-primary text-center mb-3">Forget Your Password?</a>
                      <div className="d-flex justify-content-center mt-3"><button type="button" className="btn btn-primary w-50">Sign In</button></div>
                    </form>
                  </div>
                </div>
              </div>
            </div>
          } />
          <Route path="/school" element={<School />} />
        </Routes>
        <Footer />
      </div>
    </Router>
  );
}

export default App;