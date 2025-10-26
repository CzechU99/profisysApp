import { createRouter, createWebHistory } from 'vue-router'
import { jwtDecode } from 'jwt-decode'
import DocumentsPage from '../views/DocumentsView.vue'
import LoginPage from '../views/LoginView.vue'

function isLoggedIn() {
  const token = localStorage.getItem('token')
  if (!token) return false
  
  try {
    const decoded = jwtDecode(token)
    const currentTime = Date.now() / 1000
    
    if (decoded.exp < currentTime) {
      localStorage.removeItem('token')
      localStorage.removeItem('userRole')
      return false
    }
    
    return true
  } catch (error) {
    localStorage.removeItem('token')
    localStorage.removeItem('userRole')
    return false
  }
}

const routes = [
  { 
    path: '/', 
    redirect: '/login' 
  },
  { 
    path: '/login', 
    name: 'Login', 
    component: LoginPage 
  },
  { 
    path: '/documents', 
    name: 'Documents', 
    component: DocumentsPage, 
    meta: { requiresAuth: true } 
  },
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

router.beforeEach((to, from, next) => {
  const loggedIn = isLoggedIn()
  
  if (to.meta.requiresAuth && !loggedIn) {
    next({ name: 'Login' })
  } 
  else if (to.name === 'Login' && loggedIn) {
    next({ name: 'Documents' })
  } 
  else {
    next()
  }
})

export default router