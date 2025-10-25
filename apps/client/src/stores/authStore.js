import { defineStore } from 'pinia'
import router from '../router'
import { jwtDecode } from 'jwt-decode'
import { loginUser } from '../api/authService'
import { useToast } from 'vue-toastification'
import { useDocumentsStore } from './documentsStore'
import { handleApiError } from '../utils/errorHandler'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    token: localStorage.getItem('token') || null,
    user: localStorage.getItem('user') ? JSON.parse(localStorage.getItem('user')) : null
  }),
  actions: {
    async login(username, password) {
      const toast = useToast()

      try {
        const response = await loginUser(username, password)
        
        this.token = response.data.token
        localStorage.setItem('token', this.token)

        const decoded = jwtDecode(this.token)
        this.user = {
          username: decoded.username,
          role: decoded.role 
        }

        localStorage.setItem('user', JSON.stringify(this.user))
        
        toast.success(response.data.message);
        
        router.push('/documents')
      }
      catch (error) {
        handleApiError(error)
      }
    },
    logout() {
      const toast = useToast()
      const documentsStore = useDocumentsStore()

      this.token = null
      this.user = null

      localStorage.removeItem('token')
      localStorage.removeItem('user')
      
      documentsStore.clearDocuments()
      
      toast.success("Wylogowano pomyślnie!");
      router.push('/login')
    }
  },
  getters: {
    isAdmin: (state) => state.user?.role === 'Admin',
    isUser: (state) => state.user?.role === 'User'
  }
})
