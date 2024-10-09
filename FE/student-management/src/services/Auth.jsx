import axios from 'axios';

export const loginApi = async (phone, password) => {
  return await axios.post('https://localhost:7220/api/login', {
    phone,
    password,
  });
};