import axios from 'axios'
import router from '../router'
import { useToast } from 'vue-toastification'

const api = axios.create({
  baseURL: '/api'
})

const toast = useToast()

api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token')
    if (token) {
      config.headers.Authorization = `Bearer ${token}`
    }
    return config
  },
  (error) => {
    return Promise.reject(error)
  }
)

api.interceptors.response.use(
  (response) => {
    return response
  },
  (error) => {
    if (error.response?.status === 401) {
      localStorage.removeItem('token')
      localStorage.removeItem('userRole')
      toast.error('Sesja wygasła. Zaloguj się ponownie.')
      router.push('/login')
    }
    return Promise.reject(error)
  }
)

export default api