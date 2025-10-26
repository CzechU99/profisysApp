import api from './axios'

const API_URL = import.meta.env.VITE_API_BASE_URL

export const loginUser = async (username, password) => {
  const response = await api.post(`${API_URL}/auth/login`, {username, password});
  return response;
};