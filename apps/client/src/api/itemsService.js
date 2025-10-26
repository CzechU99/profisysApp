import api from './axios'

const API_URL = import.meta.env.VITE_API_BASE_URL

export const deleteDocumentItem = async (itemId) => {
  const response = await api.delete(`${API_URL}/items/${itemId}`);
  return response;
};

export const updateItem = async (item) => {
  const response = await api.put(`${API_URL}/items`, item, {
    headers: { 'Content-Type': 'application/json' }
  });
  return response;
};

export const addItem = async (item) => {
  const response = await api.post(`${API_URL}/items`, item, {
    headers: { 'Content-Type': 'application/json' }
  });
  return response;
};
