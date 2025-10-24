import axios from 'axios';

const API_URL = import.meta.env.VITE_API_BASE_URL

axios.interceptors.request.use(config => {
  const token = localStorage.getItem('token')
  if (token) {
    config.headers.Authorization = `Bearer ${token}`
  }
  return config
})

export const deleteDocumentItem = async (itemId) => {
  const response = await axios.delete(`${API_URL}/items/${itemId}`);
  return response;
};
