import api from './axios'

const API_URL = import.meta.env.VITE_API_BASE_URL

export const getAllDocuments = async () => {
  const response = await api.get(`${API_URL}/documents`);
  return response;
};

export const fetchAllDocuments = async () => {
  const response = await api.post(`${API_URL}/dataImport/csvFiles`);
  return response;
};

export const clearAllDocuments = async () => {
  const response = await api.delete(`${API_URL}/documents`);
  return response;
};

export const deleteDocument = async (documentId) => {
  const response = await api.delete(`${API_URL}/documents/${documentId}`);
  return response;
};

export const updateDocument = async (document) => {
  const response = await api.put(`${API_URL}/documents`, document, {
    headers: { 'Content-Type': 'application/json' }
  });
  return response;
};

export const addDocument = async (document) => {
  const response = await api.post(`${API_URL}/documents`, document, {
    headers: { 'Content-Type': 'application/json' }
  });
  return response;
};