import { createRouter, createWebHistory } from 'vue-router'
import DocumentsPage from '../components/DocumentsPage.vue'
import LoginPage from '../components/LoginPage.vue'

function isLoggedIn() {
  return !!localStorage.getItem('token') 
}

const routes = [
  { path: '/', redirect: '/login' }, 
  { path: '/login', name: 'Login', component: LoginPage },
  { path: '/documents', name: 'Documents', component: DocumentsPage },
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

router.beforeEach((to, from, next) => {
  if (to.name !== 'Login' && !isLoggedIn()) {
    next({ name: 'Login' })
  } else if (to.name === 'Login' && isLoggedIn()) {
    next({ name: 'Documents' })
  } else {
    next() 
  }
})

export default router
