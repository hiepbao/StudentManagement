import axios from 'axios';

export const fetchUsers = async () => {
  return await axios.get("https://localhost:7220/api/Users/Get");
};

export const searchUsers = async (searchTerm) => {
  return await axios.get(`https://localhost:7220/api/Users/Search?searchTerm=${searchTerm}`);
};

export const registerUser = async (user) => {
  return await axios.post("https://localhost:7220/api/Users/register", user);
};