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

export const exportExcel = () => {
  return axios.get('https://localhost:7220/export-excel', { responseType: 'blob' })
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

  return axios.post('https://localhost:7220/import-excel', formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
};