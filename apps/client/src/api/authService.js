import axios from 'axios';

const API_URL = import.meta.env.VITE_API_BASE_URL

axios.interceptors.request.use(config => {
  const token = localStorage.getItem('token')
  if (token) {
    config.headers.Authorization = `Bearer ${token}`
  }
  return config
})

export const loginUser = async (username, password) => {
  try {
    const response = await axios.post(`${API_URL}/auth/login`, {username, password});
    return response.data;
  } catch (error) {
    console.error('Błąd logowania:', error);
    throw error;
  } 
};