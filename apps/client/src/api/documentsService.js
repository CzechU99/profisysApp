import axios from 'axios';

const API_URL = import.meta.env.VITE_API_BASE_URL;

export const getAllDocuments = async () => {
  try {
    const response = await axios.get(`${API_URL}/Documents/GetAllDocuments`);
    return response.data;
  } catch (error) {
    throw new Error(error.response.data);
  } 
};

export const fetchAllDocuments = async () => {
  try {
    const response = await axios.post(`${API_URL}/CsvFiles/ImportCsv`);
    return response.data;
  } catch (error) {
    throw new Error(error.response.data);
  } 
};

export const clearAllDocuments = async () => {
  try {
    const response = await axios.delete(`${API_URL}/Documents/DeleteDocuments`);
    return response.data;
  } catch (error) {
    throw new Error(error.response.data);
  } 
};

export const deleteDocument = async (id) => {
  try {
    const response = await axios.delete(`${API_URL}/Documents/DeleteDocument/${id}`);
    return response.data;
  } catch (error) {
    throw new Error(error.response.data);
  } 
};
