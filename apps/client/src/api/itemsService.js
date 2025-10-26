import api from './axios'

const API_URL = import.meta.env.VITE_API_BASE_URL

export const remove = async (itemId) => {
  return await api.delete(`${API_URL}/items/${itemId}`);
};

export const update = async (item) => {
  return await api.put(`${API_URL}/items`, item, {
    headers: { 'Content-Type': 'application/json' }
  })
};

export const create = async (item) => {
  return await await api.post(`${API_URL}/items`, item, {
    headers: { 'Content-Type': 'application/json' }
  })
};
