import { defineStore } from 'pinia'
import axios from 'axios'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    token: localStorage.getItem('token') || null,
    user: null
  }),
  actions: {
    async login(username, password) {
      const API_URL = import.meta.env.VITE_API_BASE_URL;
      const response = await axios.post(`${API_URL}/Auth/login`, { username, password })
      this.token = response.data.token
      localStorage.setItem('token', this.token)
      axios.defaults.headers.common['Authorization'] = `Bearer ${this.token}`
    },
    logout() {
      this.token = null
      localStorage.removeItem('token')
      delete axios.defaults.headers.common['Authorization']
    }
  }
})
