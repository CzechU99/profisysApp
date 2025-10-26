import { createApp } from 'vue'
import { createPinia } from 'pinia'
import './style.css'
import './assets/style/table.css'
import './assets/style/toolbar.css'
import './assets/style/loginView.css'
import App from './App.vue'
import Toast, { POSITION } from 'vue-toastification'
import 'vue-toastification/dist/index.css'
import router from './router'

import PrimeVue from 'primevue/config'
import ProgressSpinner from 'primevue/progressspinner'
import Material from '@primevue/themes/material'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import IconField from "primevue/iconfield";
import InputIcon from "primevue/inputicon";
import InputText from "primevue/inputtext";
import InputNumber from "primevue/inputnumber";
import Button from "primevue/button";
import Toolbar from 'primevue/toolbar';
import ContextMenu from 'primevue/contextmenu';
import Dialog from 'primevue/dialog'


import '@fontsource/inter'
import 'primeicons/primeicons.css'

const app = createApp(App)
const pinia = createPinia()
app.use(pinia)

app.use(Toast, {
  position: "bottom-right",
  timeout: 3000,
  closeOnClick: true,
  pauseOnFocusLoss: true,
  pauseOnHover: true,
  draggable: true,
  draggablePercent: 0.6,
  showCloseButtonOnHover: false,
  hideProgressBar: true,
  closeButton: "button",
  icon: true,
  rtl: false
});

app.use(PrimeVue, {
  theme: {
    preset: Material,
    options: {
      prefix: 'p',
      darkModeSelector: '.dark-mode',
      cssLayer: false
    }
  }
})

app.use(router)

app.component('DataTable', DataTable)
app.component('Column', Column)
app.component('InputText', InputText)
app.component('IconField', IconField)
app.component('InputIcon', InputIcon)
app.component('Toolbar', Toolbar)
app.component('Button', Button)
app.component('Dialog', Dialog)
app.component('ContextMenu', ContextMenu)
app.component('ProgressSpinner', ProgressSpinner)
app.component('InputNumber', InputNumber)

app.mount('#app')