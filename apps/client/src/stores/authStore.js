import { defineStore } from 'pinia'
import router from '../router'
import { jwtDecode } from 'jwt-decode'
import { loginUser } from '../api/authService'
import { useToast } from 'vue-toastification'
import { useDocumentsStore } from './documentsStore'

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
        this.handleApiError(error, toast)
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
    },
    handleApiError(error, toast){
      if (error.response?.data?.message) {
        toast.error(error.response.data.message);
      } else if (error.response?.status === 403) {
        toast.error("Nie masz uprawnień do tej operacji!"); 
      } else if (error.response?.status === 401) {
        toast.error("Musisz się zalogować!"); 
      } else if (error.message) {
        toast.error("Błąd połączenia z serwerem!"); 
      } else {
        toast.error("Nieznany błąd");
      }
    }
  },
  getters: {
    isAdmin: (state) => state.user?.role === 'Admin',
    isUser: (state) => state.user?.role === 'User'
  }
})
