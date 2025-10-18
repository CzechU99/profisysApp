import axios from 'axios';

const API_URL = 'http://localhost:5011/api';

export const getAllDocuments = async () => {
  try {
    const response = await axios.get(`${API_URL}/Documents/GetAllDocuments`);
    return response.data;
  } catch (error) {
    console.error('Błąd pobierania dokumentów:', error);
    throw error;
  } 
};
