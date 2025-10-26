import axios from 'axios';

const API_URL = import.meta.env.VITE_API_BASE_URL

axios.interceptors.request.use(config => {
  const token = localStorage.getItem('token')
  if (token) {
    config.headers.Authorization = `Bearer ${token}`
  }
  return config
})

export const getAllDocuments = async () => {
  const response = await axios.get(`${API_URL}/documents`);
  return response;
};

export const fetchAllDocuments = async () => {
  const response = await axios.post(`${API_URL}/dataImport/csvFiles`);
  return response;
};

export const clearAllDocuments = async () => {
  const response = await axios.delete(`${API_URL}/documents`);
  return response;
};

export const deleteDocument = async (documentId) => {
  const response = await axios.delete(`${API_URL}/documents/${documentId}`);
  return response;
};

export const updateDocument = async (document) => {
  const response = await axios.put(`${API_URL}/documents`, document, {
    headers: { 'Content-Type': 'application/json' }
  });
  return response;
};

export const addDocument = async (document) => {
  const response = await axios.post(`${API_URL}/documents`, document, {
    headers: { 'Content-Type': 'application/json' }
  });
  return response;
};