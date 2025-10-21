import { defineStore } from 'pinia'
import axios from 'axios'
import router from '../router'
import {jwtDecode} from 'jwt-decode'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    token: localStorage.getItem('token') || null,
    user: localStorage.getItem('user') ? JSON.parse(localStorage.getItem('user')) : null
  }),

  actions: {
    async login(username, password) {
      const API_URL = import.meta.env.VITE_API_BASE_URL;
      const response = await axios.post(`${API_URL}/Authentication/login`, { username, password })
      
      this.token = response.data.token
      localStorage.setItem('token', this.token)

      const decoded = jwtDecode(this.token)
      this.user = {
        username: decoded.username,
        role: decoded.role 
      }

      localStorage.setItem('user', JSON.stringify(this.user))
      router.push('/documents')
    },

    logout() {
      this.token = null
      this.user = null
      localStorage.removeItem('token')
      localStorage.removeItem('user')
      router.push('/login')
    }
  },

  getters: {
    isAdmin: (state) => state.user?.role === 'Admin',
    isUser: (state) => state.user?.role === 'User'
  }
})
