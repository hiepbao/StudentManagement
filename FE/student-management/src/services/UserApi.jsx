import axios from 'axios';
import { saveAs } from 'file-saver';

export const fetchUsers = async () => {
  return await axios.get("https://localhost:7220/api/Users/Get");
};

export const searchUsers = async (searchTerm) => {
  return await axios.get(`https://localhost:7220/api/Users/Search?searchTerm=${searchTerm}`);
};

export const registerUser = async (user) => {
  return await axios.post("https://localhost:7220/api/Users/register", user);
};

export const updateUser = async (user) => {
  return await axios.put(`https://localhost:7220/api/Users/Editfull/${user.userId}`, user);
};

export const exportExcel = () => {
  return axios.get('https://localhost:7220/api/Users/export-excel', { responseType: 'blob' })
    .then((response) => {
      const blob = new Blob([response.data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
      saveAs(blob, 'UserData.xlsx');
    })
    .catch((error) => {
      console.error('Error exporting Excel file:', error);
    });
};

export const importExcel = (file) => {
  const formData = new FormData();
  formData.append('file', file);

  return axios.post('https://localhost:7220/api/Users/import-excel', formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
};

export const addRole = async (userId, roleId) => {
  return await axios.post(`https://localhost:7220/api/Users/addRole?userId=${userId}&roleId=${roleId}`);
};