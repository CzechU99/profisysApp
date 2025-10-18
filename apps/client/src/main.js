import { createApp } from 'vue'
import './style.css'
import App from './App.vue'

import PrimeVue from 'primevue/config'
import Material from '@primevue/themes/material'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import IconField from "primevue/iconfield";
import InputIcon from "primevue/inputicon";
import InputText from "primevue/inputtext";

import '@fontsource/inter'
import 'primeicons/primeicons.css'

const app = createApp(App)

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

app.component('DataTable', DataTable)
app.component('Column', Column)
app.component('InputText', InputText)
app.component('IconField', IconField)
app.component('InputIcon', InputIcon)

app.mount('#app')